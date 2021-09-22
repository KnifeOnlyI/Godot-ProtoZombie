using Godot;

/// <summary>
/// Represent an enemy
/// </summary>
public class Enemy : KinematicBody
{
    /// <summary>
    /// The movement speed
    /// </summary>
    [Export] private float _movementSpeed = 1.0f;

    /// <summary>
    /// The path to player calculation frequency
    /// </summary>
    [Export] private float _frequencyPathCalculation = 0.5f;

    /// <summary>
    /// The attack frequency
    /// </summary>
    [Export] private float _damageFrequency = 0.5f;

    /// <summary>
    /// The amount of damage
    /// </summary>
    [Export] private float _damageQuantity = 20.0f;

    /// <summary>
    /// The maximum life
    /// </summary>
    [Export] private float _maxLife = 100.0f;

    /// <summary>
    /// The player node
    /// </summary>
    private KinematicBody _player;

    /// <summary>
    /// The body node
    /// </summary>
    private CSGMesh _body;

    /// <summary>
    /// The navigation node
    /// </summary>
    private Navigation _nav;

    /// <summary>
    /// The calculation path timer node
    /// </summary>
    private Timer _calculationPathTimer;

    /// <summary>
    /// The path positions to the player position
    /// </summary>
    private Vector3[] _path;

    /// <summary>
    /// The current path
    /// </summary>
    private int _pathNode;

    /// <summary>
    /// The delta time in seconds
    /// </summary>
    private float _delta;

    /// <summary>
    /// The current life
    /// </summary>
    private float _life;

    /// <summary>
    /// Executed on package scene
    /// </summary>
    /// <param name="player">The player</param>
    /// <param name="nav">The navigation</param>
    public void _Init(Player player, Navigation nav)
    {
        _player = player;
        _nav = nav;
    }
    
    /// <summary>
    /// Executed when the node is ready
    /// </summary>
    /// <exception cref="System.Exception">If the max life is lower or equals than 0.0f</exception>
    public override void _Ready()
    {
        if (_maxLife <= 0.0f)
        {
            throw new System.Exception("The max life MUST be greater than 0.0f");
        }

        _body = (CSGMesh) GetNode("./Body");
        _calculationPathTimer = (Timer) GetNode("./CalculationPathTimer");

        _calculationPathTimer.WaitTime = _frequencyPathCalculation;
        _life = _maxLife;
        
        MoveTo(_player.GlobalTransform.origin);

        _body.Material = new SpatialMaterial();
        
        UpdateColor();
    }

    /// <summary>
    /// Executed on every physics frame
    /// </summary>
    /// <param name="delta">The delta time in seconds</param>
    public override void _PhysicsProcess(float delta)
    {
        _delta += delta;

        if (_pathNode >= _path.Length) return;

        var direction = (_path[_pathNode] - GlobalTransform.origin);

        direction.y = 0;

        if (direction.Length() < 1)
        {
            _pathNode += 1;
        }
        else
        {
            MoveAndSlide(direction.Normalized() * _movementSpeed, Vector3.Up);
            CheckForCollides();
        }
    }

    /// <summary>
    /// Move to the specified target
    /// </summary>
    /// <param name="target">The target</param>
    private void MoveTo(Vector3 target)
    {
        _path = _nav.GetSimplePath(GlobalTransform.origin, target);

        _pathNode = 0;
    }

    /// <summary>
    /// Check for collides
    /// </summary>
    private void CheckForCollides()
    {
        for (var i = 0; i < GetSlideCount(); i++)
        {
            var collider = (Node) GetSlideCollision(i).Collider;

            if (
                collider == null ||
                !collider.IsInGroup("player") ||
                !(_delta > _damageFrequency)
            ) continue;

            _delta = 0.0f;

            OnCollideWithPlayer((Player) collider);
        }
    }

    /// <summary>
    /// Executed when collide with the player
    /// </summary>
    /// <param name="player"></param>
    private void OnCollideWithPlayer(Player player)
    {
        player.RemoveLife(_damageQuantity);
    }

    /// <summary>
    /// Update the body color according to the life
    /// </summary>
    private void UpdateColor()
    {
        ((SpatialMaterial) _body.Material).AlbedoColor = new Color(_life / _maxLife, 0.0f, 0.0f);
    }

    /// <summary>
    /// Get the life
    /// </summary>
    /// <returns>The life</returns>
    public float GetLife()
    {
        return _life;
    }
    
    /// <summary>
    /// Loose the specified amount of life
    /// </summary>
    /// <param name="value">The life to loose</param>
    public void LooseLife(float value)
    {
        _life -= value;

        if (_life <= 0.0f)
        {
            QueueFree();
        }
        else
        {
            UpdateColor();
        }
    }

    /// <summary>
    /// Executed when the path calculation timer timeout
    /// </summary>
    public void _on_Timer_timeout()
    {
        MoveTo(_player.GlobalTransform.origin);
    }
}