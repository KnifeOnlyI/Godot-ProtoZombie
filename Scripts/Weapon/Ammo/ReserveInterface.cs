namespace ProtoZombie.Scripts.Weapon.Ammo
{
    /// <summary>
    /// Represent the base interface for all ammo reserve
    /// </summary>
    public interface IReserve
    {
        /// <summary>
        /// Get the current quantity
        /// </summary>
        /// <returns>The current quantity</returns>
        ushort GetQuantity();

        /// <summary>
        /// Get the max capacity
        /// </summary>
        /// <returns>The max capacity</returns>
        ushort GetCapacity();

        /// <summary>
        /// Get the ammo type
        /// </summary>
        /// <returns>The ammo type</returns>
        AmmoType GetAmmoType();

        /// <summary>
        /// Set the quantity
        /// </summary>
        /// <param name="value">The new quantity</param>
        void SetQuantity(ushort value);

        /// <summary>
        /// Set the max capacity
        /// </summary>
        /// <param name="value">The new max capacity</param>
        ///
        /// <returns>The number of ammo cannot be stocked into the reserve</returns>
        ushort SetCapacity(ushort value);

        /// <summary>
        /// Set the ammo type
        /// </summary>
        /// <param name="value">The new ammo type</param>
        void SetAmmoType(AmmoType value);

        /// <summary>
        /// Determine if the charger is full or not
        /// </summary>
        /// <returns>TRUE if the charger is full, FALSE otherwise</returns>
        bool isFull();
        
        /// <summary>
        /// Determine if the charger is empty or not
        /// </summary>
        /// <returns>TRUE if the charger is empty, FALSE otherwise</returns>
        bool isEmpty();

        /// <summary>
        /// Add the specified quantity
        /// </summary>
        /// <param name="value">The number of ammo to add</param>
        /// <returns>The ammo quantity that could actually be gained according the capacity value</returns>
        ushort Add(ushort value);
        
        /// <summary>
        /// Remove the specified quantity
        /// </summary>
        /// <param name="value">The number of ammo to add</param>
        void Remove(ushort value);
    }
}