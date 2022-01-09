extends KinematicBody2D

export var ACC = 1000          # 加速度a(运动时不考虑摩擦力)
export var ACC_FRICTION = 700  # 摩擦力加速度at
export var MAX_SPEED = 150     # 最大速度

var velocity = Vector2.ZERO

onready var animationPlayer = $AnimationPlayer
onready var animationTree = $AnimationTree
onready var animationState = animationTree.get("parameters/playback")

func _physics_process(delta):
	var input_vector = Vector2.ZERO
	input_vector.x = Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	input_vector.y = Input.get_action_strength("ui_down") - Input.get_action_strength("ui_up")
	input_vector = input_vector.normalized()
	
	if input_vector != Vector2.ZERO:
		animationTree.set("parameters/Idle/blend_position", input_vector)
		animationTree.set("parameters/Run/blend_position", input_vector)
		velocity += input_vector * ACC * delta / 2
		velocity = velocity.clamped(MAX_SPEED)
		animationState.travel("Run")
	else:
		animationState.travel("Idle")
	velocity = velocity.move_toward(Vector2.ZERO, ACC_FRICTION * delta / 2)
		
	velocity = move_and_slide(velocity) # X = v * t + 1/2 * a * t^2
