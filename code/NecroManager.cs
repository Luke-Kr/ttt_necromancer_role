using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Sandbox;
using TerrorTown;

namespace TerrorTown
{
    public class NecroManager
    {
        // Chance that one of the innocents will be a necromancer. Default is 20%.
        [ConVar.Replicated("necromancer_role_chance", Max = 1, Min = 0, Help = "Chance that one of the traitors will be an idiot. Default is 20%.", Saved = true)]
        public static float NecromancerChance { get; set; } = 0.20f;

        [Event("Game.Team.PostSelection")]
        public static void AssignNecromancer()
        {
            if (!Game.IsServer) { return; }

            if (Game.Random.Float() < NecromancerChance)
            {
                var teamNecromancer = Teams.Get<Necromancer>();

                if (teamNecromancer == null)
                {
                    Log.Error("Team Necromancer not found! This shouldn't be possible.");
                    return;
                }

                var teamInnocent = Teams.Get<Innocent>();

                if (teamInnocent == null)
                {
                    Log.Error("Team Innocent not found! This shouldn't be possible.");
                    return;
                }

                List<TerrorTown.Player> Innocents = new (from x in Entity.All.OfType<TerrorTown.Player>()
                                            where x.Team == null
                                            orderby Game.Random.Int(0, 1000)
                                            select x);
                TerrorTown.Player ply = Innocents.FirstOrDefault();
                if (ply == null)
                {
                    Log.Info("No innocents left to assign to Team Necromancer");
                }
                // Debug log
                Log.Info("Selected " + ply.Owner.Name + " from Innocents");

                teamInnocent.RemovePlayer(ply);
                teamNecromancer.AddPlayer(ply);
            }
        }

        [Event("Player.PostSpawn")]
        public static void ApplyZombieNerfs(TerrorTown.Player ply)
        {
            if (ply.Team == Teams.Get<Zombie>())
            {
                WalkController walker = ply.MovementController as WalkController;
                walker.SpeedMultiplier = 0.4f;
                
                Zombie.DrawClothes(ply);

                foreach (Entity item in new List<Entity>(ply.Inventory.Items))
                {
                    if (!(item.GetType() == typeof(Holstered)))
                    {
                        item.Delete();
                    }
                }
                Log.Info(ply.Inventory.Items.Count);
                ZombieDeagle zeagle = new ZombieDeagle();
                ply.Inventory.AddItem(zeagle);
                ply.Inventory.MaxItems = 2;
                Log.Info(ply.Inventory.Items.Count);

                ply.Components.Add(new ZombieComponent());
            }
        }

        [Event("Player.PostOnKilled")]
        public static void PostOnKilled(DamageInfo _, TerrorTown.Player ply)
        {
            if (ply.Team == Teams.Get<Zombie>())
            {
                ply.Components.RemoveAny<ZombieComponent>();
            }
        }
    }
}
