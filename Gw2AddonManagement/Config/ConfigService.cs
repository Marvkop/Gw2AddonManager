namespace Gw2AddonManagement.Config;

public class ConfigService
{
    private const string ConfigFile = "gw2-addons/addon-config.json";

    public Config LoadConfig()
    {
        var configPath = GetConfigPath();

        if (File.Exists(configPath))
        {
            using var fileStream = File.OpenRead(configPath);
            using var reader = new StreamReader(fileStream);

            var config = JsonConvert.DeserializeObject<Config>(reader.ReadToEnd()) ?? throw new System.Exception("could not deserialize config");

            foreach (var (key, addon) in ExampleConfig.Instance.Addons)
            {
                config.Addons.TryAdd(key, addon);
            }

            ValidateConfig(config);

            return config;
        }
        else
        {
            return ExampleConfig.Instance;
        }
    }

    public void SaveConfig(Config config)
    {
        ValidateConfig(config);
        var configPath = GetConfigPath();

        if (Path.GetDirectoryName(configPath) is { } path)
        {
            Directory.CreateDirectory(path);
        }

        using var fileStream = File.Create(configPath);
        var serializeObject = JsonConvert.SerializeObject(config);
        using var writer = new StreamWriter(fileStream);
        writer.Write(serializeObject);
    }

    private string GetConfigPath()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        return $"{path}/{ConfigFile}";
    }

    private void ValidateConfig(Config config)
    {
        foreach (var (key, addon) in config.Addons)
        {
            if (addon.File is not null)
            {
                if (File.Exists(addon.File))
                {
                    config.Addons[key] = addon with { File = null, Files = new[] { addon.File } };
                }
                else
                {
                    config.Addons[key] = addon with { File = null, Files = null, Version = null };
                }
            }
            else if (addon.Files is not null)
            {
                var existingFiles = addon.Files.Where(File.Exists).ToArray();

                if (existingFiles.Length > 0)
                {
                    config.Addons[key] = addon with { Files = existingFiles };
                }
                else
                {
                    config.Addons[key] = addon with { Files = null, Version = null };
                }
            }
        }

        foreach (var (name, addon) in config.Addons)
        {
            if (!Equals(addon.Name, name))
            {
                throw new System.Exception($"invalid config, name mismatch for {name}");
            }
        }
    }
}