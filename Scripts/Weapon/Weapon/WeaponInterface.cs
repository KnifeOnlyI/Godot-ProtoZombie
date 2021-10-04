using System;
using Godot;
using ProtoZombie.Scripts.Weapon.Ammo;

namespace ProtoZombie.Scripts.Weapon.Weapon
{
    /// <summary>
    /// Represent the base interface for all weapons
    /// </summary>
    public interface IWeapon : ICloneable
    {
        /// <summary>
        /// Get the name
        /// </summary>
        /// <returns>The name</returns>
        string GetName();

        /// <summary>
        /// Get the base damage
        /// </summary>
        /// <returns>The base damage</returns>
        float GetDamage();

        /// <summary>
        /// Get the head multiplier damage
        /// </summary>
        /// <returns>The head multiplier damage</returns>
        float GetHeadMultiplier();
        
        /// <summary>
        /// Get the torso multiplier damage
        /// </summary>
        /// <returns>The torso multiplier damage</returns>
        float GetTorsoMultiplier();

        /// <summary>
        /// Get the arm multiplier damage
        /// </summary>
        /// <returns>The arm multiplier damage</returns>
        float GetArmMultiplier();

        /// <summary>
        /// Get the leg multiplier damage
        /// </summary>
        /// <returns>The leg multiplier damage</returns>
        float GetLegMultiplier();

        /// <summary>
        /// Get the firerate
        /// </summary>
        /// <returns>The firerate</returns>
        ushort GetFireRate();

        /// <summary>
        /// Get the number of ammo in the charger
        /// </summary>
        /// <returns>The number of ammo in the charger</returns>
        ushort GetAmmoInCharger();

        /// <summary>
        /// Get the maximum number of ammo in the charger
        /// </summary>
        /// <returns>The maximum number of ammo in the charger</returns>
        ushort GetMaxAmmoInCharger();

        /// <summary>
        /// Get the ammo type in the charger
        /// </summary>
        /// <returns>The ammo type in the charger</returns>
        AmmoType GetAmmoType();

        /// <summary>
        /// Get the shot type
        /// </summary>
        /// <returns>The shot type</returns>
        ShotType GetShotType();

        /// <summary>
        /// Get the charger
        /// </summary>
        /// <returns>The charger</returns>
        IReserve GetCharger();

        /// <summary>
        /// Get the shot sound
        /// </summary>
        /// <returns>The shot sound</returns>
        AudioStreamSample GetShotSound();

        /// <summary>
        /// Set the name
        /// </summary>
        /// <param name="value">The new name</param>
        void SetName(string value);
        
        /// <summary>
        /// Set the damage
        /// </summary>
        /// <param name="value">The new damage</param>
        void SetDamage(float value);

        /// <summary>
        /// Set the head damage multiplier
        /// </summary>
        /// <param name="value">The new head damage multiplier</param>
        void SetHeadMultiplier(float value);
        
        /// <summary>
        /// Set the torso damage multiplier
        /// </summary>
        /// <param name="value">The new torso damage multiplier</param>
        void SetTorsoMultiplier(float value);

        /// <summary>
        /// Set the arm damage multiplier
        /// </summary>
        /// <param name="value">The new arm damage multiplier</param>
        void SetArmMultiplier(float value);

        /// <summary>
        /// Set the leg damage multiplier
        /// </summary>
        /// <param name="value">The new leg damage multiplier</param>
        void SetLegMultiplier(float value);

        /// <summary>
        /// Set the firerate
        /// </summary>
        /// <param name="value">The new firerate</param>
        void SetFireRate(ushort value);

        /// <summary>
        /// Set the number of ammo in the charger
        /// </summary>
        /// <param name="value">The new number of ammo in the charger</param>
        void SetAmmoInCharger(ushort value);

        /// <summary>
        /// Set the maximum number of ammo in the charger.
        /// If the new value is lower than the current number of ammo in the charger, the difference is returned
        /// </summary>
        /// <param name="value">The new maximum number of ammo in the charger</param>
        /// 
        /// <returns>The number of ammo discarded from the charger</returns>
        ushort SetMaxAmmoInCharger(ushort value);

        /// <summary>
        /// Set the ammo type
        /// </summary>
        /// <param name="value">The new ammo type</param>
        void SetAmmoType(AmmoType value);

        /// <summary>
        /// Set the shot type
        /// </summary>
        /// <param name="value">The new shot typoe</param>
        void SetShotType(ShotType value);

        /// <summary>
        /// Set the shot sound
        /// </summary>
        /// <param name="value">The new shot sound</param>
        void SetShotSound(AudioStreamSample value);

        /// <summary>
        /// Shot a fire according to the specified shot type
        /// </summary>
        /// <param name="infinite">TRUE to not remove the fired ammo from the charger</param>
        /// <returns>The number of ammo has been fired</returns>
        ushort Shot(bool infinite = false);
        
        /// <summary>
        /// Shot a fire according to the shot type and put the ammo in reserve first and after the charger when
        /// the reserve's empty
        /// </summary>
        /// <param name="reserve">The reserve where get the ammo first</param>
        /// <param name="infinite">TRUE to not remove the fired ammo from the charger/reserve</param>
        /// <returns>The number of ammo fired</returns>
        ushort Shot(IReserve reserve, bool infinite = false);

        /// <summary>
        /// Reload the charger with the specified reserve
        /// </summary>
        /// <param name="reserve">The reserve where to get the ammo</param>
        /// <returns>The number of ammo reloaded from the reserve</returns>
        ushort Reload(IReserve reserve);

        /// <summary>
        /// Fetch the charger's ammo and put into the specified reserve
        /// </summary>
        /// <param name="reserve">The reserve where to put the ammo</param>
        /// <returns>The number of ammo can be fetch according to the specied reserve capacity</returns>
        ushort Fetch(IReserve reserve);

        /// <summary>
        /// Update the weapon states
        /// </summary>
        /// <param name="delta">The delta time in seconds</param>
        void Update(float delta);
    }
}