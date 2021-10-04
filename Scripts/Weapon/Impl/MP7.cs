using Godot;
using ProtoZombie.Scripts.Weapon.Ammo;
using ProtoZombie.Scripts.Weapon.Weapon;

namespace ProtoZombie.Scripts.Weapon.Impl
{
    /// <summary>
    /// Represent an MP7 weapon
    /// </summary>
    public class MP7 : AbstractWeapon
    {
        /// <summary>
        /// The shot sound
        /// </summary>
        private static readonly AudioStreamSample ShotSound =
            (AudioStreamSample) GD.Load("res://Sounds/Weapons/mp5_shot.wav");

        public MP7() :
            base("MP7", 30.0f, 2.0f, 1.0f, 0.6f, 0.8f, 720, 30, AmmoType.Mm9, ShotType.FullAuto, ShotSound)
        {
        }

        public override object Clone()
        {
            return new MP7();
        }
    }
}