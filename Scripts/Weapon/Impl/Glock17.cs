using Godot;
using ProtoZombie.Scripts.Weapon.Ammo;
using ProtoZombie.Scripts.Weapon.Weapon;

namespace ProtoZombie.Scripts.Weapon
{
    /// <summary>
    /// Represent a Glock-17 weapon
    /// </summary>
    public class Glock17 : AbstractWeapon
    {
        /// <summary>
        /// The texture
        /// </summary>
        private static readonly Texture Ttexture =
            (Texture) GD.Load("res://Textures/HUD/weapon_glock_17.png");

        /// <summary>
        /// The shot sound
        /// </summary>
        private static readonly AudioStreamSample ShotSound =
            (AudioStreamSample) GD.Load("res://Sounds/Weapons/glock17_shot.wav");

        public Glock17() :
            base(
                "Glock-17", 10.0f, 2.0f, 1.0f, 0.6f, 0.8f, 1200, 17, AmmoType.Mm9, ShotType.SemiAuto, Ttexture,
                ShotSound
            )
        {
        }
    }
}