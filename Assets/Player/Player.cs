using Godot;

/// <summary>
/// Represent the player
/// </summary>
public class Player : KinematicBody
{
    /// <summary>
    /// The mouse sensitivity
    /// </summary>
    [Export] private float _mouseSensitivity = 0.1f;

    /// <summary>
    /// The camera FOV
    /// </summary>
    [Export] private float _fov = 70.0f;
    
    /// <summary>
    /// The minimum camera angle
    /// </summary>
    [Export] private float _minCameraAngle = -89.0f;
    
    /// <summary>
    /// The maximum camera angle
    /// </summary>
    [Export] private float _maxCameraAngle = 89.0f;

    /// <summary>
    /// The move forward input
    /// </summary>
    [Export] private string _moveForwardInput = "move_forward";
    
    /// <summary>
    /// The move backward input
    /// </summary>
    [Export] private string _moveBackwardInput = "move_backward";
    
    /// <summary>
    /// The move left input
    /// </summary>
    [Export] private string _moveLeftInput = "move_left";
    
    /// <summary>
    /// The move right input
    /// </summary>
    [Export] private string _moveRightInput = "move_right";
    
    /// <summary>
    /// The run input
    /// </summary>
    [Export] private string _runInput = "run";
    
    /// <summary>
    /// The jump input
    /// </summary>
    [Export] private string _jumpInput = "jump";
    
    /// <summary>
    /// The crouch input
    /// </summary>
    [Export] private string _crouchInput = "crouch";
    
    /// <summary>
    /// The shot input
    /// </summary>
    [Export] private string _shotInput = "shot";
    
    /// <summary>
    /// The reload input
    /// </summary>
    [Export] private string _reloadInput = "reload";
    
    /// <summary>
    /// The interract input
    /// </summary>
    [Export] private string _interractInput = "interract";
    
    /// <summary>
    /// The flashlight input
    /// </summary>
    [Export] private string _flashlightInput = "flashlight";

    /// <summary>
    /// The crouch speed
    /// </summary>
    [Export] private float _crouchSpeed = 1.0f;
    
    /// <summary>
    /// The walk speed
    /// </summary>
    [Export] private float _walkSpeed = 2.0f;
    
    /// <summary>
    /// The run speed
    /// </summary>
    [Export] private float _runSpeed = 4.0f;

    /// <summary>
    /// The gravity factor
    /// </summary>
    [Export] private float _gravity = 9.81f;
    
    /// <summary>
    /// The jump strength
    /// </summary>
    [Export] private float _jumpStrength = 3.0f;

    /// <summary>
    /// TRUE to indicates the player can crouch, FALSE otherwise
    /// </summary>
    [Export] private bool _canCrouch = true;
    
    /// <summary>
    /// TRUE to indicates the player can run, FALSE otherwise
    /// </summary>
    [Export] private bool _canRun = true;
    
    /// <summary>
    /// TRUE to indicates the player can jump, FALSE otherwise
    /// </summary>
    [Export] private bool _canJump = true;
    
    /// <summary>
    /// TRUE to indicates the player can shot, FALSE otherwise
    /// </summary>
    [Export] private bool _canShot = true;
    
    /// <summary>
    /// TRUE to indicates the player can interract, FALSE otherwise
    /// </summary>
    [Export] private bool _canInterract = true;
    
    /// <summary>
    /// TRUE to indicates the player can flashlight, FALSE otherwise
    /// </summary>
    [Export] private bool _canFlashlight = true;

    /// <summary>
    /// The maximum life
    /// </summary>
    [Export] private float _maxLife = 100.0f;

    /// <summary>
    /// The head node
    /// </summary>
    private Spatial _head;
    
    /// <summary>
    /// The camera node
    /// </summary>
    private Camera _camera;
    
    /// <summary>
    /// The HUD control node
    /// </summary>
    private HUD _hud;
    
    /// <summary>
    /// The shot raycast node
    /// </summary>
    private RayCast _shotRaycast;

    /// <summary>
    /// The movement speed
    /// </summary>
    private float _movementSpeed;
    
    /// <summary>
    /// The movement velocity
    /// </summary>
    private Vector3 _velocity;
    
    /// <summary>
    /// The life 
    /// </summary>
    private float _life;

    /// <summary>
    /// Executed when the node is ready
    /// </summary>
    /// <exception cref="System.Exception">
    /// - The FOV is lower or equals than 0.0f
    /// - The max life is lower or equals than 0.0f
    /// - The crouch speed is lower or equals than 0.0f
    /// - The walk speed is lower or equals than 0.0f
    /// - The run speed is lower or equals than 0.0f
    /// </exception>
    public override void _Ready()
    {
        if (_fov <= 0.0f)
        {
            throw new System.Exception("The FOV MUST be greater than 0.0f");
        }

        if (_maxLife <= 0.0f)
        {
            throw new System.Exception("The max life MUST be greater than 0.0f");
        }

        if (_crouchSpeed <= 0.0f)
        {
            throw new System.Exception("The crouch speed MUST be greater than 0.0f");
        }

        if (_walkSpeed <= 0.0f)
        {
            throw new System.Exception("The walk speed MUST be greater than 0.0f");
        }

        if (_runSpeed <= 0.0f)
        {
            throw new System.Exception("The run speed MUST be greater than 0.0f");
        }

        _head = (Spatial) GetNode("Head");
        _camera = (Camera) _head.GetNode("Camera");
        _hud = (HUD) GetNode("HUD");
        _shotRaycast = (RayCast) _head.GetNode("ShotRaycast");
        _movementSpeed = _walkSpeed;
        _life = _maxLife;

        Input.SetMouseMode(Input.MouseMode.Captured);

        _camera.Fov = _fov;
        _hud.SetLife(_life);
        _shotRaycast.CastTo = new Vector3(0.0f, -1e6f, 0.0f);
    }

    /// <summary>
    /// Executed on every physics frame
    /// </summary>
    /// <param name="delta">The delta time in seconds</param>
    public override void _PhysicsProcess(float delta)
    {
        var direction = new Vector3();
        var inputMovementVector = new Vector2();
        var camXForm = _camera.GlobalTransform;

        if (Input.IsActionPressed(_moveForwardInput))
        {
            inputMovementVector.y += 1.0f;
        }

        if (Input.IsActionPressed(_moveBackwardInput))
        {
            inputMovementVector.y -= 1.0f;
        }

        if (Input.IsActionPressed(_moveLeftInput))
        {
            inputMovementVector.x -= 1.0f;
        }

        if (Input.IsActionPressed(_moveRightInput))
        {
            inputMovementVector.x += 1.0f;
        }

        inputMovementVector = inputMovementVector.Normalized();

        direction += -camXForm.basis.z * inputMovementVector.y;
        direction += camXForm.basis.x * inputMovementVector.x;

        // Process movement

        direction.y = 0.0f;
        direction = direction.Normalized();

        _velocity.y += delta * -_gravity;

        var target = direction * _movementSpeed;
        var velocityHorizontal = new Vector3(target.x, 0.0f, target.z);
        var acceleration = (direction.Dot(velocityHorizontal) > 0) ? 1.0 : 10.0;

        velocityHorizontal = velocityHorizontal.LinearInterpolate(target, (float) (acceleration * delta));

        _velocity.x = velocityHorizontal.x;
        _velocity.z = velocityHorizontal.z;

        _velocity = MoveAndSlide(_velocity, Vector3.Up);
    }

    /// <summary>
    /// Executed when an input is detected
    /// </summary>
    /// <param name="event">The detected event</param>
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motion && Input.GetMouseMode() == Input.MouseMode.Captured)
        {
            RotateCamera(
                motion.Relative.x * _mouseSensitivity,
                motion.Relative.y * -_mouseSensitivity
            );
        }

        if (@event.IsActionPressed(_runInput))
        {
            _movementSpeed = _runSpeed;
        }
        else if (@event.IsActionReleased(_runInput))
        {
            _movementSpeed = _walkSpeed;
        }

        if (_canJump && @event.IsActionPressed(_jumpInput) && IsOnFloor())
        {
            _velocity.y += _jumpStrength;
        }

        if (_canCrouch && @event.IsActionPressed(_crouchInput))
        {
            GD.Print("Crouch");
        }

        if (_canShot && @event.IsActionPressed(_shotInput))
        {
            var collider = (Node) _shotRaycast.GetCollider();

            if (collider != null && collider.IsInGroup("enemy"))
            {
                ((Enemy) collider).LooseLife(10.0f);
            }
        }

        if (_canShot && @event.IsActionPressed(_reloadInput))
        {
            GD.Print("Reload");
        }

        if (_canShot && @event.IsActionPressed(_shotInput))
        {
            GD.Print("Interract");
        }

        if (_canInterract && @event.IsActionPressed(_flashlightInput))
        {
            GD.Print("Flashlight");
        }
    }

    /// <summary>
    /// Rotate the camera of the specified relative XY angles
    /// </summary>
    /// <param name="x">The X degrees angle value</param>
    /// <param name="y">The Y degrees angle value</param>
    private void RotateCamera(float x, float y)
    {
        _head.RotateX(Mathf.Deg2Rad(y));
        RotateY(Mathf.Deg2Rad(-x));

        var cameraRotation = _head.RotationDegrees;

        cameraRotation.x = Mathf.Clamp(cameraRotation.x, _minCameraAngle, _maxCameraAngle);

        _head.RotationDegrees = cameraRotation;
    }

    /// <summary>
    /// Loose the specified life
    /// </summary>
    /// <param name="value">The life to loose</param>
    public void LooseLife(float value)
    {
        _life -= value;

        if (_life < 0.0f)
        {
            _life = 0.0f;
        }

        _hud.SetLife(_life);
    }
}