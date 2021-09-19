extends KinematicBody


""" The mouse sensitivity """
export var mouse_sensitivity = 0.1

""" The minimum camera angle """
export var min_camera_angle = -89.0

""" The maximum camera angle """
export var max_camera_angle = 89.0

onready var _head = $Head


func _ready():
    Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)


func _input(event) -> void:
    if event is InputEventMouseMotion and Input.get_mouse_mode() == Input.MOUSE_MODE_CAPTURED:
        _rotate_camera(
            event.relative.x * mouse_sensitivity, 
            event.relative.y * mouse_sensitivity * -1
        )


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
