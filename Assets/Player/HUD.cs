using System;
using Godot;
using System.Globalization;
using ProtoZombie.Scripts.Weapon.Ammo;

/// <summary>
/// Represent the player HUD
/// </summary>
public class HUD : Control
{
    /// <summary>
    /// The HBoxContainer contains the current weapon and ammo info
    /// </summary>
    private HBoxContainer _weaponContainer;

    /// <summary>
    /// The life label
    /// </summary>
    private Label _labelLife;

    /// <summary>
    /// The weapon name label
    /// </summary>
    private Label _labelWeaponName;

    /// <summary>
    /// The number of ammo in charger label
    /// </summary>
    private Label _labelCharger;

    /// <summary>
    /// The number of ammo in reserve label
    /// </summary>
    private Label _labelReserve;

    /// <summary>
    /// The ammo type label
    /// </summary>
    private Label _labelAmmoType;

    /// <summary>
    /// The weapon texture rect
    /// </summary>
    private TextureRect _textureRectWeapon;

    /// <summary>
    /// Executed when the node is ready
    /// </summary>
    public override void _Ready()
    {
        _weaponContainer = (HBoxContainer) GetNode("HBoxContainer2");
        _labelLife = (Label) GetNode("HBoxContainer/LifeValue");
        _labelWeaponName = (Label) GetNode("HBoxContainer2/WeaponName");
        _labelCharger = (Label) GetNode("HBoxContainer2/Charger");
        _labelReserve = (Label) GetNode("HBoxContainer2/Reserve");
        _labelAmmoType = (Label) GetNode("HBoxContainer2/AmmoType");
        _textureRectWeapon = (TextureRect) GetNode("Weapon");
    }

    /// <summary>
    /// Set the life value
    /// </summary>
    /// <param name="value">The new life value</param>
    public void SetLife(float value)
    {
        _labelLife.Text = value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Set the current weapon and ammo info visibility
    /// </summary>
    /// <param name="value">TRUE to show info, FALSE otherwise</param>
    public void SetWeaponInfoVisibility(bool value)
    {
        _weaponContainer.Visible = value;
        _textureRectWeapon.Visible = value;
    }

    /// <summary>
    /// Set the weapon name
    /// </summary>
    /// <param name="value">The new weapon name</param>
    public void SetWeaponName(string value)
    {
        _labelWeaponName.Text = value;

        
    }

    /// <summary>
    /// Set the weapon texture
    /// </summary>
    /// <param name="value">The new weapon texture</param>
    public void SetWeaponTexture(Texture value)
    {
        _textureRectWeapon.Texture = value;
    }

    /// <summary>
    /// Set the ammo type
    /// </summary>
    /// <param name="value">The new ammo type</param>
    /// <exception cref="ArgumentOutOfRangeException">If the ammo type is not managed by the function</exception>
    public void SetAmmoType(AmmoType value)
    {
        switch (value)
        {
            case AmmoType.Mm9:
                _labelAmmoType.Text = "9mm";
                break;
            case AmmoType.Acp45:
                _labelAmmoType.Text = "45 ACP";
                break;
            case AmmoType.Shotshell:
                _labelAmmoType.Text = "Shotshell";
                break;
            case AmmoType.Mm556:
                _labelAmmoType.Text = "5,56mm";
                break;
            case AmmoType.Mm762:
                _labelAmmoType.Text = "7,62mm";
                break;
            case AmmoType.Rocket:
                _labelAmmoType.Text = "Rocket";
                break;
            case AmmoType.Arrow:
                _labelAmmoType.Text = "Arrow";
                break;
            case AmmoType.CrossbowBolt:
                _labelAmmoType.Text = "Crossbow bolt";
                break;
            case AmmoType.Laser:
                _labelAmmoType.Text = "Laser";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
        }
    }

    /// <summary>
    /// Set the ammo in charger value
    /// </summary>
    /// <param name="charger">The charger</param>
    public void SetCharger(IReserve charger)
    {
        _labelCharger.Text = charger.GetQuantity().ToString();

        if (charger.isFull())
        {
            _labelCharger.AddColorOverride("font_color", Colors.Green);
        }
        else if (charger.isEmpty())
        {
            _labelCharger.AddColorOverride("font_color", Colors.Red);
        }
        else
        {
            _labelCharger.AddColorOverride("font_color", Colors.White);
        }
    }

    /// <summary>
    /// Set the ammo in reserve value
    /// </summary>
    /// <param name="reserve">The reserve</param>
    public void SetReserve(IReserve reserve)
    {
        _labelReserve.Text = reserve.GetQuantity().ToString();

        if (reserve.isFull())
        {
            _labelReserve.AddColorOverride("font_color", Colors.Green);
        }
        else if (reserve.isEmpty())
        {
            _labelReserve.AddColorOverride("font_color", Colors.Red);
        }
        else
        {
            _labelReserve.AddColorOverride("font_color", Colors.White);
        }
    }
}