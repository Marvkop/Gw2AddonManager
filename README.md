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

1. Download the client from [releases](https://github.com/Marvkop/Gw2AddonManager/releases).
2. Download the addon-config.json from the [config folder](https://github.com/Marvkop/Gw2AddonManager/tree/main/Gw2AddonManagement/Config).
3. Create a "gw2-addons"-folder in your documents-folder and move the addon-config.json inside
4. Start the client from anywhere, it doesnt need to be in any specific location.

(PS: if the client is empty the config is a the wrong place or doesnt exist.)

you can modify the config file to add your own addons if they fulfill the requirements mentioned under "Currently supported addons".

Im currently working on a way to automate step 2.and 3.
