using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.DependencyInjection;
using Gw2AddonManagement.Core;
using Gw2AddonManagement.Extensions;

namespace Gw2AddonManagement.Networking;

public class ArcDpsService
{
    private readonly FileService _fileService;
    private readonly HttpClient _client = new();
    private readonly Regex _regex = new(@"(\d{4}-\d{2}-\d{2} \d{2}:\d{2})");

    public ArcDpsService()
    {
        Ioc.Default.InitService(out _fileService);
        _client.BaseAddress = new Uri("https://www.deltaconnected.com/arcdps/x64/");
    }

    public async Task<string> Download()
    {
        var stream = await _client.GetStreamAsync("d3d11.dll");

        return _fileService.SaveToFile(stream, SaveLocation.MainFolder, "d3d11.dll");
    }

    public async Task<DateTime> GetLatestVersion()
    {
        var content = await _client.GetStringAsync((string?)null);
        var match = _regex.Match(content);
        var releases = new HashSet<string>();

        foreach (Group group in match.Groups)
        foreach (Capture capture in group.Captures)
        {
            releases.Add(capture.Value.Trim());
        }

        var toUse = releases.First();
        var year = int.Parse(toUse.Substring(0, 4));
        var month = int.Parse(toUse.Substring(5, 2));
        var day = int.Parse(toUse.Substring(8, 2));
        var hour = int.Parse(toUse.Substring(11, 2));
        var minute = int.Parse(toUse.Substring(14, 2));

        return new DateTime(year, month, day, hour, minute, 0);
    }
}