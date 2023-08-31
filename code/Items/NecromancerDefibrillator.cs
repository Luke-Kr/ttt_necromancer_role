using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Editor;
using Sandbox;
using Sandbox.Internal;

namespace TerrorTown
{
    [Title("Necro Defibrillator")]
    public partial class NecromancerDefibrillator : Weapon, IEquipment
    {
        public override AmmoType PrimaryAmmoType => AmmoType.Spare;
        [ConVar.Replicated("necromancer_zombie_charges", Min = 1, Help = "Amount of charges the Necro Defibrillator has, default is 2.", Saved = true)]
        public static int ZombieCharges { get; set; } = 2;
        public override int MaxPrimaryAmmo => ZombieCharges;
        public float ChargeTime = 5f;
        public bool Charging;
        private NecromancerDefibrillatorUI UIPanel;
        public override string WorldModelPath => "weapons/rust_pistol/rust_pistol.vmdl";

        [Net]
        public TimeSince TimeSinceMouseDown { get; set; }

        public NecromancerDefibrillator()
        {
            Droppable = false;
            DropOnDeath = false;
        }

        public override void Simulate(IClient cl)
        {
            
            base.Simulate(cl);
            if (!Game.IsServer)
            {
                return;
            }

            Ray ray = Owner.AimRay;
            float distance = 300f;
            Trace trace = Trace.Ray(in ray, in distance);
            IEntity ent = Owner;
            bool hierarchy = true;
            TraceResult traceResult = trace.Ignore(in ent, in hierarchy).Run();
            if (Input.Down("Attack1"))
            {
                if (traceResult.Entity is Corpse corpse)
                {
                    if (corpse.OwnerClient.Pawn is Player player)
                    {
                        if ((float)TimeSinceMouseDown >= ChargeTime)
                        {
                            player.LastMovement = 0f;

                            player.IdleStage = 0;


                            player.Team?.RemovePlayer(player);
                            Teams.Get<Zombie>().AddPlayer(player);
                            player.Respawn();

                            player.Position = corpse.Position;
                            

                            corpse.Delete();
                            PrimaryAmmo -= 1;
                            if (PrimaryAmmo <= 0)
                            {
                                if (Owner is Player player2)
                                {
                                    player2.Inventory?.Items.Remove(this);
                                }
                                Delete();
                            }

                            MyGame.Current.EventSystem.AddEventToLog(new BaseEvent
                            {
                                EventString = Owner.Client.Name + " revived " + (traceResult.Entity as Corpse).OwnerClient.Name,
                                Icon = "heart",
                                PlayersInvolved = new List<long>
                                {
                                    Owner.Client.SteamId,
                                    (traceResult.Entity as Corpse).OwnerClient.SteamId
                                }
                            });

                            Input.ReleaseAction("Attack1");
                        }

                        return;
                    }
                }
            }

            
            TimeSinceMouseDown = 0f;
        }

        public override void OnActiveStart()
        {
            
            base.OnActiveStart();
            
            TimeSinceMouseDown = 0f;
            if (Game.IsClient)
            {
                
                UIPanel = new NecromancerDefibrillatorUI();
                
                UIPanel.OwnerDefib = this;
                
                HUDRootPanel.Current.AddChild(UIPanel);
            }
        }

        public override void OnActiveEnd()
        {
            
            base.OnActiveEnd();
            if (Game.IsClient)
            {
                
                UIPanel.Delete();
            }
        }

    }
}