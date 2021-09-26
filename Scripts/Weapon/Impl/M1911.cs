using Godot;
using ProtoZombie.Scripts.Weapon.Ammo;
using ProtoZombie.Scripts.Weapon.Weapon;

namespace ProtoZombie.Scripts.Weapon
{
    /// <summary>
    /// Represent an M1911 weapon
    /// </summary>
    public class M1911 : AbstractWeapon
    {
        /// <summary>
        /// The texture
        /// </summary>
        private static readonly Texture Ttexture =
            (Texture) GD.Load("res://Textures/HUD/weapon_m1911.png");

        /// <summary>
        /// The shot sound
        /// </summary>
        private static readonly AudioStreamSample ShotSound =
            (AudioStreamSample) GD.Load("res://Sounds/Weapons/m1911_shot.wav");

        public M1911() :
            base("M1911", 10.0f, 2.0f, 1.0f, 0.6f, 0.8f, 342, 7, AmmoType.Acp45, ShotType.SemiAuto, Ttexture, ShotSound)
        {
        }
    }
}