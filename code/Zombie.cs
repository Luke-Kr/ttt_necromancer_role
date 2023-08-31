using Sandbox;
using Sandbox.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TerrorTown;

namespace TerrorTown
{
    public class ZombieComponent : EntityComponent<TerrorTown.Player>
    {
        public RealTimeUntil Growl = 10f;

        [GameEvent.Tick.Server]
        public void TickServer()
        {
            Game.AssertServer();
            //foreach (Entity item in new List<Entity>(Entity.Inventory.Items))
            //{
            //    if (!(item.GetType() == typeof(ZombieDeagle)) && !(item.GetType() == typeof(Holstered)))
            //    {

            //        if (item.GetType() == typeof(MagnetoStick) || item.GetType() == typeof(Crowbar))
            //        {
            //            item.Delete();
            //        }
            //        else
            //        {
            //            Entity.Inventory.DropItem(item);
            //        }

            //    }
            //}
            
            if (Growl)
            {
                Growl = Game.Random.Float(15f, 25f);
                int nr = Game.Random.Int(1, 3);
                switch (nr)
                {
                    case 1: 
                        Sound.FromEntity("zombie1", Entity);
                        break;
                    case 2:
                        Sound.FromEntity("zombie2", Entity);
                        break;
                    case 3:
                        Sound.FromEntity("zombie3", Entity);
                        break;
                }
            }

        }
    }

    public partial class Zombie : BaseTeam
    {
        public override string TeamName => "Zombie";
        public override Color TeamColour => Color.FromRgb(0x764155);
        public override TeamAlignment TeamAlignment => (TeamAlignment) 4;
        public override TeamMemberVisibility TeamMemberVisibility => TeamMemberVisibility.Alignment | TeamMemberVisibility.PublicWhenConfirmedDead;
        public override string VictimKillMessage => "You were killed by {0}. They are a Zombie.";
        public override string RoleDescription => "BRAINZZZZ.";
        public override string IdentifyString => "{0} found the body of {1}. They were a Zombie.";
        public override int TeamPlayerMaximum => 0;
        public override bool CanSeeMIA => true;
        public override bool CanSeePrivateTime => true;
        public override float TeamPlayerPercentage => 0f;
        public override bool HasTeamVoiceChat => true;

        private static readonly string Skin01 = "models/citizen/skin/citizen_skin_zombie.clothing";
        private static readonly string Skin02 = "models/citizen/skin/citizen_skin_zombie.clothing";

        public static void DrawClothes(TerrorTown.Player ply)
        {
            foreach (Clothing c in new List<Clothing>(ply.Clothing.Clothing))
            {
                ply.Clothing.Toggle(c);
            }

            Clothing clothing = Game.Random.Int(0, 1) == 1 ? GlobalGameNamespace.ResourceLibrary.Get<Clothing>(Skin01) : GlobalGameNamespace.ResourceLibrary.Get<Clothing>(Skin02);
            ply.Clothing.Toggle(clothing);
            ply.Clothing.DressEntity(ply);
            RedrawLegs(ply);
        }

        [ClientRpc]
        public static void RedrawLegs(TerrorTown.Player ply)
        {
            if (Game.LocalPawn != ply) return;

            // Delete PlayerLegs
            ply.PlayerLegs.Delete();

            // Create new PlayerLegs
            ply.CreateLegs();

            // Disable all other clothes in the new PlayerLegs
            foreach (Entity child in new List<Entity>(ply.PlayerLegs.Children))
            {
                if (child is ModelEntity modelChild)
                {
                    
                    string name = modelChild.GetModelName();
                    Log.Info(name);
                    if (name != Skin01 && name != Skin02 && name != "models/citizen/heads/head_zombie_01/models/head_zombie_01.vmdl" && name != "models/citizen/heads/head_zombie_01/models/head_zombie_02.vmdl")
                    {
                        modelChild.EnableDrawing = false;
                    }
                }
            }

        }

        [ConCmd.Admin("ttt_set_zombie")]
        private static void BecomeZombie()
        {
            if (!Game.IsServer)
            {
                ConsoleSystem.Run("ttt_set_zombie");
                return;
            }

            Player player = ConsoleSystem.Caller.Pawn as Player;
            if (player != null)
            {
                player.Team?.RemovePlayer(player);
                Teams.Get<Zombie>().AddPlayer(player);
                NecroManager.ApplyZombieNerfs(player);
            }
        }
    }
}
