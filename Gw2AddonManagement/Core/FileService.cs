using Gw2AddonManagement.Config;
using Microsoft.Win32;

namespace Gw2AddonManagement.Core;

public class FileService
{
    private readonly string _basePath;

    public FileService()
    {
        var configService = Ioc.Default.GetRequiredService<ConfigService>();
        var config = configService.LoadConfig();

        if (config.BasePath is null or "" || !File.Exists($"{config.BasePath}/Gw2-64.exe"))
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Guildwars 2 Executable (Gw2-64.exe)|Gw2-64.exe"
            };

            if (dialog.ShowDialog() is true)
            {
                config = config with { BasePath = Path.GetDirectoryName(dialog.FileName) };
                configService.SaveConfig(config);
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        _basePath = config.BasePath ?? string.Empty;
    }

    public string BasePath => _basePath;

    public string SaveToFile(Stream stream, SaveLocation location, string fileName)
    {
        EnsureFileNotPresent(fileName);

        var path = GetFullPath(location);
        Directory.CreateDirectory(path);
        using var fileStream = File.Create($"{path}/{fileName}");
        stream.CopyTo(fileStream);
        return fileStream.Name;
    }

    private string GetFullPath(SaveLocation location)
    {
        return location switch
        {
            SaveLocation.MainFolder => $"{_basePath}",
            SaveLocation.Bin64 => $"{_basePath}/bin64",
            _ => throw new ArgumentOutOfRangeException(nameof(location), location, null)
        };
    }

    public void DeleteFile(string? file)
    {
        if (file is not null && File.Exists(file))
        {
            File.Delete(file);
        }
    }

    private void EnsureFileNotPresent(string fileName)
    {
        foreach (var location in Enum.GetValues<SaveLocation>())
        {
            DeleteFile($"{GetFullPath(location)}/{fileName}");
        }
    }
}