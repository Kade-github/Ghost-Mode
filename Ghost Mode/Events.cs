using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost_Mode
{
    class Events : IEventHandlerRoundStart, IEventHandlerWaitingForPlayers, IEventHandlerCheckRoundEnd, IEventHandlerPlayerDie
    {
        private readonly GhostMode plugin;

        public static bool WaitingForPlayers = false;
        public static bool GhostRound = false;
        public static Player TheGhost { get; set; }

        public Events(GhostMode plugin) => this.plugin = plugin;

        public void OnRoundStart(RoundStartEvent ev)
        {
            WaitingForPlayers = false;
            if (GhostRound)
            {
                // Start choosing players

                List<Player> players = plugin.Server.GetPlayers();
                int Numbers = 0;
                foreach (Player p in players)
                {
                    if (TheGhost == null)
                        TheGhost = p;
                    else
                    {
                        switch(Numbers)
                        {
                            case 0:
                                p.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
                                p.PersonalBroadcast(10, "You are the commander of <color=yellow>TEAMX</color>, <color=red>Exterminate the ghost!</color> Lead your team to victory!",true);
                                p.AddHealth(300);
                                Numbers++;
                                break;
                            case 1:
                                p.ChangeRole(Role.NTF_LIEUTENANT, true, true, false, true);
                                p.PersonalBroadcast(10, "You are the 1st Lieutenant of <color=yellow>TEAMX</color>, <color=red>Exterminate the ghost!</color> And follow your commander to victory!", true);
                                p.AddHealth(250);
                                Numbers++;
                                break;
                            case 2:
                                p.ChangeRole(Role.NTF_LIEUTENANT, true, true, false, true);
                                p.PersonalBroadcast(10, "You are the 2nd Lieutenant of <color=yellow>TEAMX</color>, <color=red>Exterminate the ghost!</color> And follow your fellow Lieutenants and your Commander to Victory!", true);
                                p.AddHealth(200);
                                Numbers++;
                                break;
                            case 3:
                                p.ChangeRole(Role.NTF_LIEUTENANT, true, true, false, true);
                                p.PersonalBroadcast(10, "You are the 3rd Lieutenant of <color=yellow>TEAMX</color>, <color=red>Exterminate the ghost!</color> And follow your fellow Lieutenants and your Commander to Victory!", true);
                                p.AddHealth(150);
                                Numbers++;
                                break;
                            default:
                                p.ChangeRole(Role.NTF_CADET, true, true, false, true);
                                p.PersonalBroadcast(10, "You are a member of <color=yellow>TEAMX</color>, <color=red>Exterminate the ghost!</color> And follow your fellow Units and your Commander to Victory!", true);
                                p.AddHealth(100);
                                break;
                        }
                    }
                }
                plugin.Server.Map.AnnounceCustomMessage("M T F . T E A M . X Entered . . AllRemaining");
                TheGhost.AddHealth(500);
                TheGhost.GiveItem(ItemType.USP);
                TheGhost.PersonalBroadcast(10, "<color=red>YOU ARE THE GHOST!</color> <color=yellow>You are invisible! But when you talk they can see you!</color> <color=blue>Kill them all!</color>", true);
                TheGhost.SetGhostMode(true, true, true);
                TheGhost.ChangeRole(Role.TUTORIAL, true, true, false, true);
            }
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev) => WaitingForPlayers = true;

        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (ev.Status != ROUND_END_STATUS.ON_GOING)
                GhostRound = false;
        }

        public void OnPlayerDie(PlayerDeathEvent ev)
        {
            if (ev.Player == TheGhost)
            {
                ev.Player.OverwatchMode = true;
                plugin.Server.Map.Broadcast(10, "<color=red>THE GHOST HAS BEEN EXTERMINATED!</color>", true);
            }
            else
            {
                ev.Player.OverwatchMode = true;
                ev.Player.PersonalBroadcast(10, "<color=red>Oops you died! You can no longer respawn!</color>", true);
            }
        }
    }
}
