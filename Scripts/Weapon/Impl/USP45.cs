using Godot;
using ProtoZombie.Scripts.Weapon.Ammo;
using ProtoZombie.Scripts.Weapon.Weapon;

namespace ProtoZombie.Scripts.Weapon.Impl
{
    /// <summary>
    /// Represent an USP45 weapon
    /// </summary>
    public class USP45 : AbstractWeapon
    {
        /// <summary>
        /// The shot sound
        /// </summary>
        private static readonly AudioStreamSample ShotSound =
            (AudioStreamSample) GD.Load("res://Sounds/Weapons/m1911_shot.wav");

        public USP45() :
            base("USP45", 10.0f, 2.0f, 1.0f, 0.6f, 0.8f, 342, 7, AmmoType.Mm9, ShotType.SemiAuto, ShotSound)
        {
        }

        public override object Clone()
        {
            return new USP45();
        }
    }
}