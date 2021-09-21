using Godot;
using System.Globalization;

/// <summary>
/// Represent the player HUD
/// </summary>
public class HUD : Control
{
    /// <summary>
    /// The life label
    /// </summary>
    private Label _labelLife;

    /// <summary>
    /// Executed when the node is ready
    /// </summary>
    public override void _Ready()
    {
        _labelLife = (Label) GetNode("HBoxContainer/LifeValue");
    }

    /// <summary>
    /// Set the life value
    /// </summary>
    /// <param name="value">The new life value</param>
    public void SetLife(float value)
    {
        _labelLife.Text = value.ToString(CultureInfo.InvariantCulture);
    }
}
