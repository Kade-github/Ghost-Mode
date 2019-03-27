using Smod2;
using Smod2.Attributes;
using Smod2.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost_Mode
{
    [PluginDetails(
    author = "Kade",
    name = "G.H.O.S.T M.O.D.E",
    description = "A Ghost Gamemode.",
    id = "kade.gm.ghost",
    version = "1.0",
    SmodMajor = 3,
    SmodMinor = 3,
    SmodRevision = 1
    )]
    public class GhostMode : Plugin
    {
        public override void OnDisable()
        {
            Info("Disabling spookyness!");
        }

        public override void OnEnable()
        {
            Info("\nGhost Mode is activated, please type 'ghost' in the RA console to start it up.\n(DO THIS ON THE WAITING FOR PLAYERS SCENE)");
        }

        public override void Register()
        {
            AddConfig(new ConfigSetting("ghost_enable", true, SettingType.BOOL, true, "Enable or Disable this plugin!"));

            AddCommand("ghost_start", new GamemodeStart(this));
            AddEventHandlers(new Events(this));
        }
    }
}
