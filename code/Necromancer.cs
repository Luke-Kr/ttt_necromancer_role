using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.Internal;

namespace TerrorTown
{
    public class Necromancer : BaseTeam
    {
        public override string TeamName => "Necromancer";
        public override Color TeamColour => Color.FromRgb(0x764155);
        public override TeamAlignment TeamAlignment => (TeamAlignment) 4;
        public override TeamMemberVisibility TeamMemberVisibility => TeamMemberVisibility.Alignment | TeamMemberVisibility.PublicWhenConfirmedDead;
        public override string VictimKillMessage => "You were killed by {0}. They are the Necromancer.";
        public override string RoleDescription => "You are a NECROMANCER! Kill all other players to win and build an army of zombies to help you.";
        public override string IdentifyString => "{0} found the body of {1}. They were the Necromancer.";
        public override int TeamPlayerMaximum => 1;
        public override int TeamPlayerMinimum => 0;
        public override bool CanSeeMIA => true;
        public override string OverheadIcon => "ui/world/necromancer.png";
        public override bool CanSeePrivateTime => true;
        public override float TeamPlayerPercentage => 0f;
        public override bool EnableBuyMenu => true;
        public override bool BuyMenuTraitorItems => true;
        public override bool HasTeamVoiceChat => true;


        public override void AddPlayer(Player player)
        {
            base.AddPlayer(player);
            player.Credits = 1;
            player.Inventory.AddItem(new NecromancerDefibrillator());
        }

        [ConCmd.Admin("ttt_set_necromancer")]
        private static void BecomeNecromancer()
        {
            if (!Game.IsServer)
            {
                ConsoleSystem.Run("ttt_set_necromancer");
                return;
            }

            Player player = ConsoleSystem.Caller.Pawn as Player;
            if (player != null)
            {
                player.Team?.RemovePlayer(player);
                Teams.Get<Necromancer>().AddPlayer(player);
            }
        }
    }
}
