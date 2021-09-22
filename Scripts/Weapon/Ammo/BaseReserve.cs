using Godot;

namespace ProtoZombie.Scripts.Weapon.Ammo
{
    /// <summary>
    /// The base reserve
    /// </summary>
    public class BaseReserve : IReserve
    {
        /// <summary>
        /// The current quantity
        /// </summary>
        private ushort _quantity;

        /// <summary>
        /// The maximum capacity
        /// </summary>
        private ushort _capacity;

        /// <summary>
        /// The ammo type
        /// </summary>
        private AmmoType _ammoType;

        /// <summary>
        /// Create a full new reserve with the specified max ammo capacity
        /// </summary>
        /// <param name="capacity">The maximum capacity</param>
        /// <param name="ammoType">The ammo type</param>
        public BaseReserve(ushort capacity, AmmoType ammoType)
        {
            SetCapacity(capacity);
            SetQuantity(capacity);
            SetAmmoType(ammoType);
        }

        public BaseReserve(ushort quantity, ushort capacity, AmmoType ammoType)
        {
            SetCapacity(capacity);
            SetQuantity(quantity);
            SetAmmoType(ammoType);
        }

        public ushort GetQuantity()
        {
            return _quantity;
        }

        public ushort GetCapacity()
        {
            return _capacity;
        }

        public AmmoType GetAmmoType()
        {
            return _ammoType;
        }

        public void SetQuantity(ushort value)
        {
            if (value > GetCapacity())
            {
                throw new System.Exception("The quantity MUST BE lower or equals than capacity");
            }

            _quantity = value;
        }

        public ushort SetCapacity(ushort value)
        {
            if (value == 0)
            {
                throw new System.Exception("The capacity MUST BE greater than 0");
            }
            
            ushort toRemove = 0;

            if (value < _quantity)
            {
                toRemove = (ushort) (_quantity - value);

                _quantity = value;
            }

            _capacity = value;

            return toRemove;
        }

        public void SetAmmoType(AmmoType value)
        {
            _ammoType = value;
        }

        public bool isFull()
        {
            return _quantity == _capacity;
        }

        public bool isEmpty()
        {
            return _quantity == 0;
        }

        public ushort Add(ushort value)
        {
            var toAdd = value;

            if (_quantity + toAdd > _capacity)
            {
                toAdd = (ushort) (_capacity - _quantity);
            }

            SetQuantity((ushort) (GetQuantity() + toAdd));
            
            return toAdd;
        }

        public void Remove(ushort value)
        {
            SetQuantity((ushort) (GetQuantity() - value));
        }
    }
}