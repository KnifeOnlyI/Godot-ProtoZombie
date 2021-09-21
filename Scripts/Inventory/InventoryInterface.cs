using System.Collections.Generic;
using ProtoZombie.Scripts.Weapon.Ammo;
using ProtoZombie.Scripts.Weapon.Weapon;

namespace ProtoZombie.Scripts.Inventory
{
    /// <summary>
    /// The base interface for all inventories
    /// </summary>
    public interface IInventory
    {
        /// <summary>
        /// Get the number of weapons
        /// </summary>
        /// <returns>The number of weapons</returns>
        byte GetNbWeapons();
        
        /// <summary>
        /// Get the reserve corresponding to the specified ammo type
        /// </summary>
        /// <param name="ammoType">The ammo type to search</param>
        /// <returns>The reserve corresponding to the specified ammo type</returns>
        IReserve GetReserve(AmmoType ammoType);

        /// <summary>
        /// Get the weapon at the specified index
        /// </summary>
        /// <param name="index">The index of weapon to get</param>
        /// <returns>The founded weapon</returns>
        IWeapon GetWeapon(byte index);
        
        /// <summary>
        /// Add the specified weapon to the inventory
        /// </summary>
        /// <param name="weapon">The weapon to add</param>
        void AddWeapon(IWeapon weapon);

        /// <summary>
        /// Remove the weapon at the specified index
        /// </summary>
        /// <param name="index">The index of weapon to remove</param>
        /// <returns>The removed weapon</returns>
        IWeapon RemoveWeapon(byte index);

        /// <summary>
        /// Remove the specified weapon
        /// </summary>
        /// <param name="weapon">The reference of weapon to remove</param>
        /// <returns>The removed reference</returns>
        IWeapon RemoveWeapon(IWeapon weapon);

        /// <summary>
        /// Remove all weapons
        /// </summary>
        void RemoveAllWeapons();
        
        /// <summary>
        /// Get the index of the specified weapon
        /// </summary>
        /// <param name="weapon">The weapon to found</param>
        ///
        /// <returns>The index of founded weapon</returns>
        byte GetWeaponIndex(IWeapon weapon);
        
        /// <summary>
        /// Check if the specified weapon index is valid or not
        /// </summary>
        /// <param name="index">The index to check</param>
        /// <returns>TRUE if the weapon exists, FALSE otherwise</returns>
        bool WeaponExists(byte index);
    }
}