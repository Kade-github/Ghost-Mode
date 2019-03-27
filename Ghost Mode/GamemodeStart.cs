using Smod2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost_Mode
{
    class GamemodeStart : ICommandHandler
    {
        private readonly GhostMode plugin;

        public GamemodeStart(GhostMode plugin) => this.plugin = plugin;

        public string GetCommandDescription()
        {
            return "Starts the Gamemode";
        }

        public string GetUsage()
        {
            return "GHOST_START";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!Events.WaitingForPlayers)
                return new string[] { "Sorry!","Right now the server is not on the WaitingForPlayers Scene!","So the gamemode will not start..." };
            else
            {
                Events.GhostRound = true;
                plugin.Info("THIS IS A GHOST ROUND!");
                plugin.Server.Map.Broadcast(10, "THIS IS A <color=red>GHOST ROUND!</color> <color=yellow>GET READY FOR SPOOKS!</color>", true);
                return new string[] { "The Spooky ghost's have been activated!" };
            }
        }
    }
}
