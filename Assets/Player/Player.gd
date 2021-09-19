extends KinematicBody

# Exported variables ###########################################################

""" The mouse sensitivity """
export var mouse_sensitivity = 0.1

""" The FOV """
export var fov = 70.0

""" The minimum camera angle """
export var min_camera_angle = -89.0

""" The maximum camera angle """
export var max_camera_angle = 89.0

""" Move forward input """
export var move_forward_input = "move_forward"

""" Move backward input """
export var move_backward_input = "move_backward"

""" Move left input """
export var move_left_input = "move_left"

""" Move right input """
export var move_right_input = "move_right"

""" Crouch speed """
export var crouch_speed = 1.0

""" Walk speed """
export var walk_speed = 2.0

""" Run speed """
export var run_speed = 4.0

""" The gravity factor """
export var gravity = 9.81

""" The jump strenght """
export var jump_strength = 3.0

""" The flag indicate if the player can crouch or not """
export var can_crouch = true

""" The flag indicate if the player can run or not """
export var can_run = true

""" The flag indicate if the player can jump or not """
export var can_jump = true

""" The flag indicate if the player can shot or not """
export var can_shot = true

""" The flag indicate if the player can interract or not """
export var can_interract = true

""" The flag indicate if the player can flashlight or not """
export var can_flashlight = true

""" The max life value """
export var max_life = 100.0

# Onready variables ############################################################

""" The head spatial node """
onready var _head = $Head

""" The main camera node """
onready var _camera = $Head/Camera

""" The HUD """
onready var _hud = $HUD

# Private variables ############################################################

""" The current movement speed """
var _movement_speed = walk_speed

""" The velocity """
var _velocity = Vector3()

""" The life """
var _life = max_life

# Engine override ##############################################################

"""
Executed when node is ready
"""
func _ready() -> void:
    assert(fov > 0.0, "The FOV MUST be greater than 0.0")
    assert(max_life > 0.0, "The max life MUST be greater than 0.0")

    Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

    _camera.set_fov(fov)
    _hud.set_life(_life)


"""
Executed every physics process frames

:param delta: The delta time in seconds
"""
func _physics_process(delta: float) -> void:
    var direction = Vector3()
    var input_movement_vector = Vector2()
    var cam_xform = _camera.get_global_transform()

    if Input.is_action_pressed(move_forward_input):
        input_movement_vector.y += 1.0
    elif Input.is_action_pressed(move_backward_input):
        input_movement_vector.y -= 1.0
    
    if Input.is_action_pressed(move_left_input):
        input_movement_vector.x -= 1.0
    elif Input.is_action_pressed(move_right_input):
        input_movement_vector.x += 1.0

    input_movement_vector = input_movement_vector.normalized()
    
    direction += -cam_xform.basis.z * input_movement_vector.y
    direction += cam_xform.basis.x * input_movement_vector.x

    # Process movement

    direction.y = 0.0
    direction = direction.normalized()

    _velocity.y += delta * -gravity

    var velocity_h = _velocity
    var target = direction * _movement_speed
    velocity_h = target
    
    # Interpolate
    velocity_h.y = 0
    var acceleration = 1.0 if direction.dot(velocity_h) > 0 else 10.0
    velocity_h = velocity_h.linear_interpolate(target, acceleration * delta)

    # Move and slide
    _velocity.x = velocity_h.x
    _velocity.z = velocity_h.z
    _velocity = move_and_slide(_velocity, Vector3(0, 1, 0), 0.05, 4)


"""
Executed when an input is detected
"""
func _input(event) -> void:
    if event is InputEventMouseMotion and Input.get_mouse_mode() == Input.MOUSE_MODE_CAPTURED:
        _rotate_camera(
            event.relative.x * mouse_sensitivity,
            event.relative.y * -mouse_sensitivity
        )

    if event.is_action_pressed("run"):
        _movement_speed = run_speed
    elif event.is_action_released("run"):
        _movement_speed = walk_speed

    if can_jump and event.is_action_pressed("jump") and is_on_floor():
        _velocity.y += jump_strength

    if can_crouch and event.is_action_pressed("crouch"):
        print("Crouch")

    if can_shot and event.is_action_pressed("shot"):
        print("Shot")

    if can_shot and event.is_action_pressed("reload"):
        print("Reload")

    if can_interract and event.is_action_pressed("interract"):
        print("Interract")

    if can_interract and event.is_action_pressed("flashlight"):
        print("Flashlight")

# Public functions #############################################################

"""
Get the life

:return: The life
"""
func get_life() -> float:
    return _life


"""
Get the max life

:return: The max life
"""
func get_max_life() -> float:
    return max_life


"""
The life to loose

:param value: The life to loose
"""
func loose_life(value: float) -> void:
    _life -= value

    if _life < 0.0:
        _life = 0.0

    _hud.set_life(_life)

"""
The life to gain

:param value: The life to gain
"""
func gain_life(value: float) -> void:
    _life += value

    if _life > max_life:
        _life = max_life

    _hud.set_life(_life)

# Private functions ############################################################

"""
Rotate the camera of the specified relative degrees values

:param x: The X relative degrees value (Negative = Turn left, Positive = Turn right)
:param y: The Y relative degrees value (Negative = Look down, Positive = Look up)
"""
func _rotate_camera(x: float, y: float) -> void:
    _head.rotate_x(deg2rad(y))
    rotate_y(deg2rad(-x))

    var camera_rot = _head.rotation_degrees
    
    camera_rot.x = clamp(camera_rot.x, min_camera_angle, max_camera_angle)
    _head.rotation_degrees = camera_rot
