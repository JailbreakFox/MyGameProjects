extends Area2D

onready var invincible = false setget set_invincible, get_invincible
onready var timer = $Timer

signal invincibility_started
signal invincibility_stoped

func start_invincible(duration):
	self.invincible = true
	timer.start(duration)

func set_invincible(value):
	if invincible == value:
		return
		
	invincible = value
		
	if invincible == true:
		emit_signal("invincibility_started")
	else:
		emit_signal("invincibility_stoped")
		
func get_invincible():
	return invincible
	
func _on_Timer_timeout():
	self.invincible = false
	
func _on_Hurtbox_invincibility_started():
	set_deferred("monitoring", false)
	
func _on_Hurtbox_invincibility_stoped():
	set_deferred("monitoring", true)
