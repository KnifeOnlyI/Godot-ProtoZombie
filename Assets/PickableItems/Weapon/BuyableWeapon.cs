using Godot;
using ProtoZombie.Scripts.Weapon;
using ProtoZombie.Scripts.Weapon.Impl;
using ProtoZombie.Scripts.Weapon.Weapon;

public class BuyableWeapon : Area
{
    /// <summary>
    /// The weapon (enum) to represent
    /// </summary>
    [Export] private Weapons _weapon = 0;

    /// <summary>
    /// The price
    /// </summary>
    [Export] private uint _price = 500;

    /// <summary>
    /// The 3D label node
    /// </summary>
    private Spatial _label;

    /// <summary>
    /// The buyable weapon
    /// </summary>
    private IWeapon _buyableWeapon;

    /// <summary>
    /// Executed when the node is ready
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException">If the weapon is not managed</exception>
    public override void _Ready()
    {
        _label = GetNode<Spatial>("./Label3D");

        switch (_weapon)
        {
            case Weapons.Usp45:
                _buyableWeapon = new USP45();
                break;
            case Weapons.Mp7:
                _buyableWeapon = new MP7();
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }

        _label.Call("set_text", _buyableWeapon.GetName());
    }

    /// <summary>
    /// Get the buyable weapon informations
    /// </summary>
    /// <returns>The buyable weapon informations</returns>
    public IWeapon GetWeapon()
    {
        return _buyableWeapon;
    }

    /// <summary>
    /// Get an cloned instance of the buyable weapon
    /// </summary>
    /// <returns>A cloned instance of the buyable weapon</returns>
    public IWeapon BuyWeapon()
    {
        return (IWeapon) _buyableWeapon.Clone();
    }

    /// <summary>
    /// Get the price
    /// </summary>
    /// <returns>The price</returns>
    public uint GetPrice()
    {
        return _price;
    }
}