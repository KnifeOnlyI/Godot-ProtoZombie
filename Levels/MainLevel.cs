using Godot;

/// <summary>
/// Represent the main level
/// </summary>
public class MainLevel : Spatial
{
    /// <summary>
    /// The maximum number of enemies can exists at same time
    /// </summary>
    [Export] private ushort _maxNbEnemies = 3;

    /// <summary>
    /// The enemy packaged scene
    /// </summary>
    private PackedScene _enemyScene;

    /// <summary>
    /// The player node
    /// </summary>
    private Player _player;

    /// <summary>
    /// The navigation node
    /// </summary>
    private Navigation _nav;

    /// <summary>
    /// The enemy counter
    /// </summary>
    private ushort _enemyCounter;

    /// <summary>
    /// Executed when the node is ready
    /// </summary>
    public override void _Ready()
    {
        _enemyScene = (PackedScene) ResourceLoader.Load("res://Assets/Enemy/Enemy.tscn");
        _player = (Player) GetNode("Player");
        _nav = (Navigation) GetNode("Navigation");
    }

    /// <summary>
    /// Spawn a new enemy at the center of the map
    /// </summary>
    private void SpawnNewEnemy()
    {
        // TODO Fix the lag when a new enemy is instanced 
        
        var newEnemy = (Enemy) _enemyScene.Instance();

        newEnemy._Init(_player, _nav);

        AddChild(newEnemy);

        _enemyCounter++;
    }

    /// <summary>
    /// Executed on timout of the enemy spawn timer
    /// </summary>
    public void _OnEnemySpawnTimerTimeout()
    {
        if (_enemyCounter < _maxNbEnemies)
        {
            SpawnNewEnemy();
        }
    }
}