using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using ProtoZombie.Scripts.Weapon.Ammo;
using ProtoZombie.Scripts.Weapon.Weapon;

namespace ProtoZombie.Scripts.Inventory
{
    /// <summary>
    /// Represent an inventory
    /// </summary>
    public class Inventory : IInventory
    {
        /// <summary>
        /// The weapons list
        /// </summary>
        private readonly List<IWeapon> _weapons = new List<IWeapon>();

        /// <summary>
        /// The ammo reserves list
        /// </summary>
        private readonly Dictionary<AmmoType, IReserve> _ammoReserves = new Dictionary<AmmoType, IReserve>();

        /// <summary>
        /// Create a new inventory without weapons and all empty reserves
        /// </summary>
        public Inventory()
        {
            _ammoReserves.Add(AmmoType.Mm9, new BaseReserve(0, 30, AmmoType.Mm9));
            _ammoReserves.Add(AmmoType.Acp45, new BaseReserve(0, 30, AmmoType.Acp45));
            _ammoReserves.Add(AmmoType.Shotshell, new BaseReserve(0, 16, AmmoType.Shotshell));
            _ammoReserves.Add(AmmoType.Mm556, new BaseReserve(0, 300, AmmoType.Mm556));
            _ammoReserves.Add(AmmoType.Mm762, new BaseReserve(0, 300, AmmoType.Mm762));
            _ammoReserves.Add(AmmoType.Rocket, new BaseReserve(0, 2, AmmoType.Rocket));
            _ammoReserves.Add(AmmoType.Arrow, new BaseReserve(0, 15, AmmoType.Arrow));
            _ammoReserves.Add(AmmoType.CrossbowBolt, new BaseReserve(0, 15, AmmoType.CrossbowBolt));
            _ammoReserves.Add(AmmoType.Laser, new BaseReserve(0, 100, AmmoType.Laser));
        }

        public byte GetNbWeapons()
        {
            return (byte) _weapons.Count;
        }

        public IReserve GetReserve(AmmoType ammoType)
        {
            if (!_ammoReserves.ContainsKey(ammoType))
            {
                throw new System.Exception($"Not available reserve for the specified ammo type : {ammoType}");
            }

            return _ammoReserves[ammoType];
        }

        public IWeapon GetWeapon(byte index)
        {
            if (index > _weapons.Count - 1)
            {
                throw new System.Exception($"Index out of bounds for weapons list : {index}");
            }

            return _weapons[index];
        }

        public IWeapon GetWeaponByName(string name)
        {
            IWeapon results = null;

            var index = _weapons.FindIndex(q => q.GetName() == name);

            if (index != -1)
            {
                results = GetWeapon((byte) index);
            }

            return results;
        }

        public Dictionary<AmmoType, IReserve> GetAllReserves()
        {
            return _ammoReserves;
        }

        public void AddWeapon(IWeapon weapon)
        {
            _weapons.Add(weapon);
        }

        public IWeapon RemoveWeapon(byte index)
        {
            var weapon = GetWeapon(index);

            _weapons.RemoveAt(index);

            return weapon;
        }

        public IWeapon RemoveWeapon(IWeapon weapon)
        {
            _weapons.Remove(weapon);

            return weapon;
        }

        public void RemoveAllWeapons()
        {
            _weapons.Clear();
        }

        public byte GetWeaponIndex(IWeapon weapon)
        {
            var index = _weapons.FindIndex(q => q == weapon);

            if (index == -1)
            {
                throw new System.Exception($"Index out of bounds for weapons list : {index}");
            }

            return (byte) index;
        }

        public bool WeaponExists(byte index)
        {
            return index <= _weapons.Count - 1;
        }
    }
}