using Godot;
using ProtoZombie.Scripts.Weapon.Ammo;

namespace ProtoZombie.Scripts.Weapon.Weapon
{
    /// <summary>
    /// Represent a base weapon
    /// </summary>
    public abstract class AbstractWeapon : IWeapon
    {
        /// <summary>
        /// The name
        /// </summary>
        private string _name;

        /// <summary>
        /// The amount of damage
        /// </summary>
        private float _damage;

        /// <summary>
        /// The head damage multiplier
        /// </summary>
        private float _headMultiplier;

        /// <summary>
        /// The torso damage multiplier
        /// </summary>
        private float _torsoMultiplier;

        /// <summary>
        /// The arm damage multiplier
        /// </summary>
        private float _armMultiplier;

        /// <summary>
        /// The leg damage multiplier
        /// </summary>
        private float _legMultiplier;

        /// <summary>
        /// The firerate (rounds per minutes)
        /// </summary>
        private ushort _fireRate;

        /// <summary>
        /// The shot type
        /// </summary>
        private ShotType _shotType;

        /// <summary>
        ///  The charger
        /// </summary>
        private readonly IReserve _charger;

        /// <summary>
        /// The texture
        /// </summary>
        private Texture _texture;

        /// <summary>
        /// The shot sound
        /// </summary>
        private AudioStreamSample _shotSound;
        
        /// <summary>
        /// The delta for shot in seconds
        /// </summary>
        private float _deltaShot;

        /// <summary>
        /// The firerate delta based on the firerate (rounds per minutes)
        /// </summary>
        private readonly float _fireRateDelta;

        /// <summary>
        /// Create a new weapon
        /// </summary>
        /// 
        /// <param name="name">The name</param>
        /// <param name="damage">The amount of damage</param>
        /// <param name="headMultiplier">The head damage multiplier</param>
        /// <param name="torsoMultiplier">The torso damage multiplier</param>
        /// <param name="armMultiplier">The arm damage multiplier</param>
        /// <param name="legMultiplier">The leg damage multiplier</param>
        /// <param name="fireRate">The firerate</param>
        /// <param name="maxAmmoInCharger">The maximum ammo in charger</param>
        /// <param name="ammoType">The ammo type</param>
        /// <param name="shotType">The shot type</param>
        /// <param name="texture">The texture</param>
        /// <param name="shotSound">The shot sound</param>
        protected AbstractWeapon(string name, float damage, float headMultiplier, float torsoMultiplier,
            float armMultiplier, float legMultiplier, ushort fireRate, ushort maxAmmoInCharger, AmmoType ammoType,
            ShotType shotType, Texture texture = null, AudioStreamSample shotSound = null)
        {
            SetName(name);
            SetDamage(damage);
            SetHeadMultiplier(headMultiplier);
            SetTorsoMultiplier(torsoMultiplier);
            SetArmMultiplier(armMultiplier);
            SetLegMultiplier(legMultiplier);
            SetFireRate(fireRate);
            SetShotType(shotType);
            SetTexture(texture);
            SetShotSound(shotSound);

            _charger = new BaseReserve(maxAmmoInCharger, ammoType);

            _fireRateDelta = 1.0f / (_fireRate / 60.0f);
            _deltaShot = _fireRateDelta;
        }

        public string GetName()
        {
            return _name;
        }

        public float GetDamage()
        {
            return _damage;
        }

        public float GetHeadMultiplier()
        {
            return _headMultiplier;
        }

        public float GetTorsoMultiplier()
        {
            return _torsoMultiplier;
        }

        public float GetArmMultiplier()
        {
            return _armMultiplier;
        }

        public float GetLegMultiplier()
        {
            return _legMultiplier;
        }

        public ushort GetFireRate()
        {
            return _fireRate;
        }

        public ushort GetAmmoInCharger()
        {
            return _charger.GetQuantity();
        }

        public ushort GetMaxAmmoInCharger()
        {
            return _charger.GetCapacity();
        }

        public AmmoType GetAmmoType()
        {
            return _charger.GetAmmoType();
        }

        public ShotType GetShotType()
        {
            return _shotType;
        }

        public IReserve GetCharger()
        {
            return _charger;
        }

        public Texture GetTexture()
        {
            return _texture;
        }

        public AudioStreamSample GetShotSound()
        {
            return _shotSound;
        }

        public void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new System.Exception("The name CANNOT be NULL");
            }

            _name = value;
        }

        public void SetDamage(float value)
        {
            if (value < 0.0f)
            {
                throw new System.Exception("The damage CANNOT be lower than 0.0f");
            }

            _damage = value;
        }

        public void SetHeadMultiplier(float value)
        {
            if (value < 0.0f)
            {
                throw new System.Exception("The head multiplier CANNOT be lower than 0.0f");
            }

            _headMultiplier = value;
        }

        public void SetTorsoMultiplier(float value)
        {
            if (value < 0.0f)
            {
                throw new System.Exception("The torso multiplier CANNOT be lower than 0.0f");
            }

            _torsoMultiplier = value;
        }

        public void SetArmMultiplier(float value)
        {
            if (value < 0.0f)
            {
                throw new System.Exception("The arm multiplier CANNOT be lower than 0.0f");
            }

            _armMultiplier = value;
        }

        public void SetLegMultiplier(float value)
        {
            if (value < 0.0f)
            {
                throw new System.Exception("The leg multiplier CANNOT be lower than 0.0f");
            }

            _legMultiplier = value;
        }

        public void SetFireRate(ushort value)
        {
            if (value <= 0.0f)
            {
                throw new System.Exception("The firerate MUST be greater than 0.0f");
            }

            _fireRate = value;
        }

        public void SetAmmoInCharger(ushort value)
        {
            _charger.SetQuantity(value);
        }

        public ushort SetMaxAmmoInCharger(ushort value)
        {
            return _charger.SetCapacity(value);
        }

        public void SetAmmoType(AmmoType value)
        {
            _charger.SetAmmoType(value);
        }

        public void SetShotType(ShotType value)
        {
            _shotType = value;
        }

        public void SetTexture(Texture value)
        {
            _texture = value;
        }

        public void SetShotSound(AudioStreamSample value)
        {
            _shotSound = value;
        }

        public ushort Shot(bool infinite = false)
        {
            ushort toUse = 0;

            if (_deltaShot < _fireRateDelta || _charger.GetQuantity() <= 0)
            {
                return toUse;
            }

            _deltaShot = 0.0f;
            
            toUse = 1;

            if (!infinite)
            {
                _charger.SetQuantity((ushort) (_charger.GetQuantity() - toUse));
            }

            return toUse;
        }

        public ushort Shot(IReserve reserve, bool infinite = false)
        {
            ushort toUse = 0;

            if (_deltaShot < _fireRateDelta)
            {
                return toUse;
            }

            _deltaShot = 0.0f;
            
            toUse = 1;

            if (!infinite)
            {
                _charger.SetQuantity((ushort) (_charger.GetQuantity() - toUse));
            }
            else if (reserve.GetQuantity() > 0)
            {
                reserve.SetQuantity((ushort) (reserve.GetQuantity() - 1));
            }
            else if (_charger.GetQuantity() > 0)
            {
                _charger.SetQuantity((ushort) (_charger.GetQuantity() - 1));
            }
            else
            {
                toUse = 0;
            }

            return toUse;
        }

        public ushort Reload(IReserve reserve)
        {
            if (reserve.GetAmmoType() != _charger.GetAmmoType())
            {
                throw new System.Exception(
                    "The ammo type of the specified reserver doesn't corresponds with the weapon ammo type"
                );
            }

            var toReload = (ushort) (_charger.GetCapacity() - _charger.GetQuantity());

            if (toReload > reserve.GetQuantity())
            {
                toReload = reserve.GetQuantity();
            }

            reserve.SetQuantity((ushort) (reserve.GetQuantity() - toReload));
            _charger.SetQuantity((ushort) (_charger.GetQuantity() + toReload));

            return toReload;
        }

        public ushort Fetch(IReserve reserve)
        {
            if (reserve.GetAmmoType() != _charger.GetAmmoType())
            {
                throw new System.Exception(
                    "The ammo type of the specified reserver doesn't corresponds with the weapon ammo type"
                );
            }

            var toFetch = _charger.GetQuantity();

            if (reserve.GetQuantity() + toFetch > reserve.GetCapacity())
            {
                toFetch = (ushort) (reserve.GetCapacity() - reserve.GetQuantity());
            }

            _charger.SetQuantity((ushort) (_charger.GetQuantity() - toFetch));
            reserve.SetQuantity((ushort) (reserve.GetQuantity() + toFetch));

            return toFetch;
        }

        public void Update(float delta)
        {
            _deltaShot += delta;
        }
    }
}