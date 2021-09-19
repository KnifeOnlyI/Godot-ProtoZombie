extends KinematicBody

# Exported variables ###########################################################

""" The movement speed """
export var move_speed = 1.0

""" The frequency of path calculation  """
export var frequency_path_calculation = 0.1

""" The frequency damage """
export var damage_frequency = 0.5

""" The amont of damage for each attack """
export var damage_quantity = 20.0

# Onready variables ############################################################

""" The player """
onready var _player = $"../../Player"

""" The navigation mesh """
onready var _nav = get_parent()

""" The calculation path calculation timer """
onready var _calculation_path_timer = $CalculationPathTimer

# Private variables ############################################################

""" The path list """
var _path = []

""" The current path node """
var _path_node = 0

""" The delta time """
var _delta = 0.0

# Engine override ##############################################################

"""
Executed when node is ready
"""
func _ready() -> void:
    _calculation_path_timer.wait_time = frequency_path_calculation


"""
Executed every physics process frames

:param delta: The delta time in seconds
"""
func _physics_process(delta) -> void:
    _delta += delta

    if _path_node < _path.size():
        var direction = (_path[_path_node] - global_transform.origin)

        direction.y = 0
        
        if direction.length() < 1:
            _path_node += 1
        else:
            # warning-ignore:return_value_discarded
            move_and_slide(direction.normalized() * move_speed, Vector3.UP)
            _check_for_collides()

# Private functions ############################################################

"""
Move to the specified position

:param target_pos: The position where to move
"""
func move_to(target_pos) -> void:
    _path = _nav.get_simple_path(global_transform.origin, target_pos)

    _path_node = 0


"""
Check for any collisions
"""
func _check_for_collides() -> void:
    for i in get_slide_count():
        var collider = get_slide_collision(i).collider

        if collider.is_in_group("player") \
            and _delta > damage_frequency:
            _delta = 0.0

            _on_collide_with_player(collider)


"""
Executed when a collide with the player is detected

:param player: The player
"""
func _on_collide_with_player(player) -> void:
    player.loose_life(damage_quantity);

# Signals ######################################################################

"""
Executed on timer timeout
"""
func _on_Timer_timeout() -> void:
    move_to(_player.global_transform.origin)
