using System.Reflection;
using Gw2AddonManagement.Data;
using Gw2AddonManagement.Extensions;
using Gw2AddonManagement.Util;

namespace Gw2AddonManagement.Updater;

public static class UpdateManager
{
    private const string Format = "yyyy-MM-ddTHH:mm:ssZ";

    public static void CheckForUpdate()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Gw2AddonManagement by Marvkop");

        var url = $"{GitHubHelper.GetBaseUri("Marvkop", "Gw2AddonManager")}/releases/latest";

        var awaiter = client.GetAsync(url).GetAwaiter();
        var message = awaiter.GetResult();

        if (message.StatusCode is not HttpStatusCode.OK)
            return;

        var response = message.GetContentAs<GitHubLatestReleaseResponse>();
        var assembly = Assembly.GetExecutingAssembly();
        var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        var version = attribute.InformationalVersion.Split('+')[1];

        var assemblyCreationDate = DateTime.ParseExact(version, Format, CultureInfo.InvariantCulture).AddMinutes(5);
        var latestReleaseCreationDate = DateTime.ParseExact(response.Created, Format, CultureInfo.InvariantCulture);

        if (assemblyCreationDate < latestReleaseCreationDate)
        {
            var location = AppDomain.CurrentDomain.BaseDirectory;
            var assetString = client.GetStringAsync(response.AssetsUrl).Result;
            var assetResponse = JsonConvert.DeserializeObject<GitHubAssetResponse[]>(assetString)?[0];

            using (var downloadStream = client.GetStreamAsync(assetResponse.DownloadUrl).Result)
            using (var fileStream = File.Create($"{location}/Gw2AddonManagementUpdate.exe"))
            {
                downloadStream.CopyTo(fileStream);
            }

            using (var batFileStream = File.Create($"{location}/Gw2AddonManagementUpdate.bat"))
            using (var writer = new StreamWriter(batFileStream))
            {
                writer.WriteLine("@ECHO OFF");
                writer.WriteLine("TIMEOUT /t 1 /nobreak > NUL");
                writer.WriteLine("TASKKILL /IM \"{0}\" > NUL", "Gw2AddonManagement.exe");
                writer.WriteLine("MOVE \"{0}\" \"{1}\"", $"{location}/Gw2AddonManagementUpdate.exe", $"{location}/Gw2AddonManagement.exe");
                writer.WriteLine("DEL \"%~f0\" & START \"\" /B \"{0}\"", $"{location}/Gw2AddonManagement.exe");
            }

            var startInfo = new ProcessStartInfo($"{location}/Gw2AddonManagementUpdate.bat")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = Path.GetDirectoryName(location)
            };

            Process.Start(startInfo);
            Environment.Exit(0);
        }
    }
}