using Gw2AddonManagement.Core;

namespace Gw2AddonManagement.Config;

public static class ExampleConfig
{
    public static readonly Config Instance = new(null, new Dictionary<string, Addon>());

    static ExampleConfig()
    {
        AddAddon(new Addon(
            "arc_dps",
            "arcdps",
            null,
            null,
            SaveLocation.MainFolder,
            new Dictionary<string, string>()
        ));

        AddAddon(new Addon(
            "arcdps_unofficial_extras_releases",
            "github",
            null,
            null,
            SaveLocation.MainFolder,
            new Dictionary<string, string>
            {
                { "repo", "arcdps_unofficial_extras_releases" },
                { "owner", "Krappa322" },
            }
        ));
        
        AddAddon(new Addon(
            "arcdps_healing_stats",
            "github",
            null,
            null,
            SaveLocation.MainFolder,
            new Dictionary<string, string>
            {
                { "repo", "arcdps_healing_stats" },
                { "owner", "Krappa322" },
            }
        ));
        
        AddAddon(new Addon(
            "GW2-ArcDPS-Boon-Table",
            "github",
            null,
            null,
            SaveLocation.MainFolder,
            new Dictionary<string, string>
            {
                { "repo", "GW2-ArcDPS-Boon-Table" },
                { "owner", "knoxfighter" },
            }
        ));
        
        AddAddon(new Addon(
            "arcdps-killproof.me-plugin",
            "github",
            null,
            null,
            SaveLocation.MainFolder,
            new Dictionary<string, string>
            {
                { "repo", "arcdps-killproof.me-plugin" },
                { "owner", "knoxfighter" },
            }
        ));

        AddAddon(new Addon(
            "GW2-ArcDPS-Mechanics-Log",
            "github",
            null,
            null,
            SaveLocation.MainFolder,
            new Dictionary<string, string>
            {
                { "repo", "GW2-ArcDPS-Mechanics-Log" },
                { "owner", "knoxfighter" },
            }
        ));

        AddAddon(new Addon(
            "arcdps-squad-ready-plugin",
            "github",
            null,
            null,
            SaveLocation.MainFolder,
            new Dictionary<string, string>
            {
                { "repo", "arcdps-squad-ready-plugin" },
                { "owner", "cheahjs" },
            }
        ));

        AddAddon(new Addon(
            "arcdps-food-reminder",
            "github",
            null,
            null,
            SaveLocation.MainFolder,
            new Dictionary<string, string>
            {
                { "repo", "arcdps-food-reminder" },
                { "owner", "Zerthox" },
            }
        ));
    }

    private static void AddAddon(Addon addon)
    {
        Instance.Addons.Add(addon.Name, addon);
    }
}