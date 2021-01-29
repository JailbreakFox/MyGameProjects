extends RigidBody2D

export var min_speed = 100  # Minimum speed range.
export var max_speed = 200  # Maximum speed range.

func _ready():
	var mob_types = $AnimatedSprite.frames.get_animation_names()
	$AnimatedSprite.animation = mob_types[randi() % mob_types.size()]
	$AnimatedSprite.play()

func _on_VisibilityNotifier2D_screen_exited():
	queue_free()
