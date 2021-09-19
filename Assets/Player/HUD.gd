extends Control

# Onready variables ############################################################

""" The life label """
onready var label_life = $HBoxContainer/LifeValue

# Public functions #############################################################

"""
Set the life value

:param value: The new life value
"""
func set_life(value: float) -> void:
    label_life.set_text(str(value))
