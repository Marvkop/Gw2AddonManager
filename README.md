# Gw2AddonManager
I am not associated with Guildwars 2 or Arenanet or any of the supported addons.

This is just a little personal side-project by me and not intended to be "THE ONE AND ONLY" addon manager.

Use at your own risk if you want to.

Implemented with WPF in C#.

## Currently supported addons:

Supports all addons that release on github as .dll

Examples used in my personal config:

- arc_dps
- arcdps_unofficial_extras
- arcdps_healing_stats
- arcdps-killproof.me-plugin
- GW2-ArcDPS-Boon-Table

## Setup

Requires Windows and atleast [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet)

1. Download the latest version of the manager from [releases](https://github.com/Marvkop/Gw2AddonManager/releases).
2. Start the manager from anywhere, it doesnt need to be in any specific location.
3. On first startup the program will create a file under "my-documents"/gw2-addons/addon-config.json

You can modify the config file to add your own addons if they fulfill the requirements mentioned under "Currently supported addons".

To reset the config, just delete it (this wont delete already installed addons) or press the "delete all" button in the manager.

The manager wont detect/modify already existing addons and will just overwrite them if you download them through the manager.

### Manually deleting addons, managed by the manager may lead to undefined behaviour!