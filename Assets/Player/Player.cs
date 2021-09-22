using Godot;
using ProtoZombie.Scripts;
using ProtoZombie.Scripts.Inventory;
using ProtoZombie.Scripts.Weapon;
using ProtoZombie.Scripts.Weapon.Ammo;
using ProtoZombie.Scripts.Weapon.Weapon;

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
    /// The fetch ammo input
    /// </summary>
    [Export] private string _fetchAmmoInput = "fetch_ammo";

    /// <summary>
    /// The switch weapon up input
    /// </summary>
    [Export] private string _switchWeaponUpInput = "switch_weapon_up";

    /// <summary>
    /// The switch weapon down input
    /// </summary>
    [Export] private string _switchWeaponDownInput = "switch_weapon_down";

    /// <summary>
    /// The unequip weapon input
    /// </summary>
    [Export] private string _unequipWeaponInput = "unequip_weapon";

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
    /// TRUE to indicates the player can fetch ammo, FALSE otherwise (FALSE if the can shot is FALSE)
    /// </summary>
    [Export] private bool _canFetchAmmo = true;

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
    /// The points
    /// </summary>
    [Export] private uint _points = 500;

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
    /// The shot audio stream
    /// </summary>
    private AudioStreamPlayer3D _shotAudioStream;

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
    /// The inventory
    /// </summary>
    private readonly IInventory _inventory = new Inventory();

    /// <summary>
    /// The current equiped weapon
    /// </summary>
    private IWeapon _weapon;

    /// <summary>
    /// Determine if in shot or not (used for full-auto shot type)
    /// </summary>
    private bool _inShot;

    /// <summary>
    /// Executed when the node is ready
    /// </summary>
    /// <exception cref="System.Exception">
    /// - The FOV is lower or equals than 0.0f
    /// - The max life is lower or equals than 0.0f
    /// - The crouch speed is lower or equals than 0.0f
    /// - The walk speed is lower or equals than 0.0f
    /// - The run speed is lower or equals than 0.0f*
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

        Input.SetMouseMode(Input.MouseMode.Captured);

        _head = (Spatial) GetNode("Head");
        _camera = (Camera) _head.GetNode("Camera");
        _hud = (HUD) GetNode("HUD");
        _shotRaycast = (RayCast) _head.GetNode("ShotRaycast");
        _shotAudioStream = (AudioStreamPlayer3D) GetNode("ShotAudioStream");
        _movementSpeed = _walkSpeed;
        _life = _maxLife;
        _camera.Fov = _fov;
        _shotRaycast.CastTo = new Vector3(0.0f, -1e6f, 0.0f);
        _canFetchAmmo = _canFetchAmmo && _canShot;

        _inventory.GetReserve(AmmoType.Mm9).SetQuantity(30);
        _inventory.AddWeapon(new Glock17());
        _inventory.AddWeapon(new Mp5());

        UpdateHud();
    }

    /// <summary>
    /// Executed on every physics frame
    /// </summary>
    /// <param name="delta">The delta time in seconds</param>
    public override void _PhysicsProcess(float delta)
    {
        if (_canShot)
        {
            ProcessWeapons(delta);
        }

        ProcessMovement(delta);
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

        if (_canRun && @event.IsActionPressed(_runInput))
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

        if (_canShot && @event.IsActionPressed(_reloadInput))
        {
            Reload();
        }

        if (_canFetchAmmo && @event.IsActionPressed(_fetchAmmoInput))
        {
            FetchAmmo();
        }

        if (_canShot && @event.IsActionPressed(_switchWeaponUpInput))
        {
            SwitchWeaponUp();
        }

        if (_canShot && @event.IsActionPressed(_switchWeaponDownInput))
        {
            SwitchWeaponDown();
        }

        if (_canShot && @event.IsActionPressed(_unequipWeaponInput))
        {
            UnequipWeapon();
        }

        if (_canInterract && @event.IsActionPressed(_interractInput))
        {
            GD.Print("Interract");
        }

        if (_canFlashlight && @event.IsActionPressed(_flashlightInput))
        {
            GD.Print("Flashlight");
        }
    }

    /// <summary>
    /// Remove the specified life
    /// </summary>
    /// <param name="value">The life to remove</param>
    public void RemoveLife(float value)
    {
        SetLife(GetLife() - value);
    }

    /// <summary>
    /// Add the specified life
    /// </summary>
    /// <param name="value">The life to gain</param>
    /// <returns>The life quantity that could actually be added according the max life value</returns>
    public float AddLife(float value)
    {
        var toUse = value;

        if (_life + toUse > _maxLife)
        {
            toUse = _maxLife - _life;
        }

        SetLife(GetLife() + toUse);

        return toUse;
    }

    /// <summary>
    /// Add the specified amount of ammo to the specified reserve
    /// </summary>
    /// <param name="ammoType">The ammo type</param>
    /// <param name="value">The value to add</param>
    /// <returns>The ammo quantity that could actually be added according the reserve capacity</returns>
    public ushort AddAmmo(AmmoType ammoType, ushort value)
    {
        var added = _inventory.GetReserve(ammoType)?.Add(value) ?? 0;

        UpdateWeaponHud();

        return added;
    }

    /// <summary>
    /// Get the life
    /// </summary>
    /// <returns>The life</returns>
    private float GetLife()
    {
        return _life;
    }

    /// <summary>
    /// Set the life
    /// </summary>
    /// <param name="value">The new life value</param>
    private void SetLife(float value)
    {
        _life = value;

        if (_life < 0.0f)
        {
            _life = 0.0f;
        }

        _hud.SetLife(_life);
    }

    /// <summary>
    /// Set the points
    /// </summary>
    /// <param name="value">The new points value</param>
    private void SetPoints(uint value)
    {
        _points = value;

        _hud.SetPoints(_points);
    }

    /// <summary>
    /// Process the weapons actions according to the specified delta
    /// </summary>
    /// <param name="delta">The delta in seconds</param>
    private void ProcessWeapons(float delta)
    {
        _weapon?.Update(delta);

        // TODO Set a shot end sound to avoid jerks when shot multiple times

        if (Input.IsActionPressed(_shotInput))
        {
            Shot();
        }
        else if (Input.IsActionJustReleased(_shotInput))
        {
            _inShot = false;
        }
    }

    /// <summary>
    /// Process the movement according to the specified delta
    /// </summary>
    /// <param name="delta">The delta in seconds</param>
    private void ProcessMovement(float delta)
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
    /// Perform a shot with the current equiped weapon
    /// </summary>
    private void Shot()
    {
        if (_weapon == null || _weapon.GetShotType() == ShotType.SemiAuto && _inShot || _weapon.Shot() <= 0) return;

        _inShot = true;

        _shotAudioStream.Play();

        UpdateWeaponHud();

        var collider = (Node) _shotRaycast.GetCollider();

        if (collider is Enemy enemy)
        {
            ShotOnEnemy(enemy);
        }
    }

    /// <summary>
    /// Perform a shot on the specified enemy
    /// </summary>
    /// <param name="enemy"></param>
    private void ShotOnEnemy(Enemy enemy)
    {
        var pointsToWin = Constants.PointsPerShot;

        enemy.LooseLife(_weapon.GetDamage());

        if (enemy.GetLife() <= 0.0f)
        {
            pointsToWin += Constants.PointsPerKill;
        }

        SetPoints(_points + pointsToWin);
    }

    /// <summary>
    /// Reload the current equiped weapon
    /// </summary>
    private void Reload()
    {
        if (_weapon?.Reload(_inventory.GetReserve(_weapon.GetAmmoType())) <= 0) return;

        UpdateWeaponHud();
    }

    /// <summary>
    /// Fetch the ammo in the current equiped weapon and put into the current equiped reserve
    /// </summary>
    private void FetchAmmo()
    {
        if (_weapon?.Fetch(_inventory.GetReserve(_weapon.GetAmmoType())) <= 0) return;

        UpdateWeaponHud();
    }

    /// <summary>
    /// Switch the weapon up
    /// </summary>
    private void SwitchWeaponUp()
    {
        if (_inventory.GetNbWeapons() <= 0)
        {
            return;
        }

        if (_weapon == null)
        {
            _weapon = _inventory.GetWeapon(0);
        }
        else
        {
            var nextIndex = (byte) (_inventory.GetWeaponIndex(_weapon) + 1);

            _weapon = _inventory.GetWeapon(_inventory.WeaponExists(nextIndex) ? nextIndex : (byte) 0);
        }

        _shotAudioStream.Stream = _weapon.GetShotSound();

        UpdateWeaponHud();
    }

    /// <summary>
    /// Switch the weapon down
    /// </summary>
    private void SwitchWeaponDown()
    {
        if (_inventory.GetNbWeapons() <= 0)
        {
            return;
        }

        if (_weapon == null)
        {
            _weapon = _inventory.GetWeapon(0);
        }
        else
        {
            var nextIndex = _inventory.GetWeaponIndex(_weapon) - 1;

            _weapon = nextIndex < 0
                ? _inventory.GetWeapon((byte) (_inventory.GetNbWeapons() - 1))
                : _inventory.GetWeapon((byte) nextIndex);
        }

        _shotAudioStream.Stream = _weapon.GetShotSound();

        UpdateWeaponHud();
    }

    /// <summary>
    /// Unequip the current equiped weapon
    /// </summary>
    private void UnequipWeapon()
    {
        _weapon = null;

        UpdateWeaponHud();
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
    /// Update the HUD
    /// </summary>
    private void UpdateHud()
    {
        UpdateWeaponHud();

        _hud.SetLife(_life);
        _hud.SetMaxLife(_maxLife);
    }

    /// <summary>
    /// Update the HUD according to the current equiped weapon/reserve
    /// </summary>
    private void UpdateWeaponHud()
    {
        if (_weapon != null)
        {
            _hud.SetWeaponInfoVisibility(true);
            _hud.SetWeaponName(_weapon.GetName());
            _hud.SetWeaponTexture(_weapon.GetTexture());
            _hud.SetAmmoType(_weapon.GetAmmoType());
            _hud.SetCharger(_weapon.GetCharger());
            _hud.SetReserve(_inventory.GetReserve(_weapon.GetAmmoType()));
        }
        else
        {
            _hud.SetWeaponInfoVisibility(false);
        }
    }
}