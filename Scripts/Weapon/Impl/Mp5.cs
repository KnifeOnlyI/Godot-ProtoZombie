using Godot;
using ProtoZombie.Scripts.Weapon.Ammo;
using ProtoZombie.Scripts.Weapon.Weapon;

namespace ProtoZombie.Scripts.Weapon
{
    /// <summary>
    /// Represent an MP5 weapon
    /// </summary>
    public class Mp5 : AbstractWeapon
    {
        /// <summary>
        /// The texture
        /// </summary>
        private static readonly Texture Ttexture =
            (Texture) GD.Load("res://Textures/HUD/weapon_mp5.png");

        /// <summary>
        /// The shot sound
        /// </summary>
        private static readonly AudioStreamSample ShotSound =
            (AudioStreamSample) GD.Load("res://Sounds/Weapons/mp5_shot.wav");

        public Mp5() :
            base("MP5", 25.0f, 2.0f, 1.0f, 0.6f, 0.8f, 800, 30, AmmoType.Mm9, ShotType.FullAuto, Ttexture, ShotSound)
        {
        }
    }
}