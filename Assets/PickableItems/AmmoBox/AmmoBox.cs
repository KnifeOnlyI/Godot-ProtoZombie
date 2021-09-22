using Godot;
using ProtoZombie.Scripts.Weapon.Ammo;

/// <summary>
/// Represent an ammo box
/// </summary>
public class AmmoBox : Area
{
    /// <summary>
    /// The ammo quantity available
    /// </summary>
    [Export] private ushort _quantity = 100;

    /// <summary>
    /// The ammo type
    /// </summary>
    [Export] private AmmoType _ammoType = AmmoType.Mm9;

    /// <summary>
    /// Executed when an other node entered into area
    /// </summary>
    /// <param name="node">The node entered</param>
    public void OnBodyEntered(Node node)
    {
        if (node is Player player)
        {
            OnPlayerEntered(player);
        }
    }

    /// <summary>
    /// Executed when a player entered into area
    /// </summary>
    /// <param name="player">The player</param>
    private void OnPlayerEntered(Player player)
    {   
        _quantity -= player.AddAmmo(_ammoType, _quantity);

        if (_quantity <= 0.0)
        {
            QueueFree();
        }
    }
}