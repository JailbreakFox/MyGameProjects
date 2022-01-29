extends Node

export(int) var MAX_HEALTH = 4
onready var health = MAX_HEALTH setget set_health, get_health

signal no_health
signal health_changed(value)

func set_health(value):
	if health == value:
		return
		
	health = value
	emit_signal("health_changed", health)
	
	if (0 >= value):
		emit_signal("no_health")
	
func get_health():
	return health
