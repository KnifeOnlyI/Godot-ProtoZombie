using System.Collections.Generic;
using Godot;

/// <summary>
/// Represent the main level
/// </summary>
public class MainLevel : Spatial
{
    /// <summary>
    /// The maximum number of enemies can exists at same time
    /// </summary>
    [Export] private ushort _maxNbEnemies = 20;

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
    /// The enemy spawner node list
    /// </summary>
    private readonly List<Position3D> _enemySpawnerList = new List<Position3D>();

    /// <summary>
    /// The enemies pool
    /// </summary>
    private readonly List<Enemy> _enemiesPool = new List<Enemy>();

    /// <summary>
    /// Executed when the node is ready
    /// </summary>
    public override void _Ready()
    {
        _enemyScene = (PackedScene) ResourceLoader.Load("res://Assets/Enemy/Enemy.tscn");
        _player = (Player) GetNode("Player");
        _nav = (Navigation) GetNode("Navigation");

        foreach (var child in _nav.GetChildren())
        {
            if (child is Position3D node && node.IsInGroup("enemy_spawner"))
            {
                _enemySpawnerList.Add(node);
            }
        }
        
        CreateEnemyPool();
    }

    /// <summary>
    /// Create the enemies pool
    /// </summary>
    private void CreateEnemyPool()
    {
        for (var i = 0; i < _maxNbEnemies; i++)
        {
            var enemy = (Enemy) _enemyScene.Instance();

            enemy._Init(_player, _nav, _enemySpawnerList);

            _enemiesPool.Add(enemy);

            AddChild(enemy);
        }
    }

    /// <summary>
    /// Spawn a new enemy at the center of the map
    /// </summary>
    private void SpawnNewEnemy()
    {
        var index = _enemiesPool.FindIndex(q => !q.IsActivated());

        if (index != -1)
        {
            _enemiesPool[index].Activate();
        }
    }

    /// <summary>
    /// Executed on timout of the enemy spawn timer
    /// </summary>
    public void _OnEnemySpawnTimerTimeout()
    {
        SpawnNewEnemy();
    }
}