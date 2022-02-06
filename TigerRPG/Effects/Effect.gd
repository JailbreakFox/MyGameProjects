extends AnimatedSprite

func _ready():
	connect("animation_finished", self, "on_animation_finished")
	frame = 0
	play("Animate")

func on_animation_finished():
	queue_free()
