using Sandbox;

namespace TerrorTown
{
    [Title("Zeagle")]
    public class ZombieDeagle : Gun, ISecondaryWeapon
    {
        public override string ViewModelPath => "models/weapons/v_deagle.vmdl";
        public override string WorldModelPath => "models/weapons/w_deagle.vmdl";
        public override float PrimaryAttackDelay => 0.6f;
        public override int MaxPrimaryAmmo => 7;
        public override bool Automatic => true;
        public override float HeadshotMultiplier => 4f;
        public override AmmoType PrimaryAmmoType => AmmoType.Spare;

        public ZombieDeagle()
        {
            Droppable = false;
            DropOnDeath = false;
        }

        public override void PrimaryAttack()
        {
            base.PrimaryAmmo--;
            ShootBullet(37f, 0.008f, 37f);
            PlaySound("deagle.shoot");
            (Owner as AnimatedEntity)?.SetAnimParameter("b_attack", value: true);
            ShootEffects();
            if (Game.IsClient)
            {
                DoViewPunch(6f);
            }
            if (base.PrimaryAmmo == 0)
            {
                Owner.TakeDamage(new DamageInfo { Damage = float.MaxValue });
            }
        }
    }
}
