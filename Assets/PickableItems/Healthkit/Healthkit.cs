using Godot;

/// <summary>
/// Represent an healthkit
/// </summary>
public class Healthkit : Area
{
    /// <summary>
    /// The life quantity available
    /// </summary>
    [Export] private float _quantity = 100.0f;

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
        _quantity -= player.AddLife(_quantity);

        if (_quantity <= 0.0)
        {
            QueueFree();
        }
    }
}