extends KinematicBody2D

export var ACC = 1000           # 加速度a(运动时不考虑摩擦力)
export var ACC_FRICTION = 700   # 摩擦力加速度at
export var MAX_SPEED = 120      # 最大速度
export var ROLL_SPEED = 800     # 翻滚速度

enum {
	DEFAULT,
	MOVE,
	ROLL,
	ATTACK
}

const HitEffect = preload("res://Effects/HitEffect.tscn")

var input_vector = Vector2.ZERO # 输入速度
var velocity = Vector2.ZERO     # 运行速度
var state = MOVE                # 状态机状态
var stat = PlayerStat

onready var hurtbox = $Hurtbox
onready var animationPlayer = $AnimationPlayer
onready var animationTree = $AnimationTree
onready var animationState = animationTree.get("parameters/playback")

func _ready():
	stat.connect("no_health", self, "queue_free")

func _process(delta):
	move(delta)
	
	# 动画状态机
	match state:
		MOVE:
			move_state()
		ROLL:
			roll_state()
		ATTACK:
			attack_state()
		DEFAULT:
			pass
	
# player位置控制
func move(delta):
	input_vector.x = Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	input_vector.y = Input.get_action_strength("ui_down") - Input.get_action_strength("ui_up")
	input_vector = input_vector.normalized()
	
	if input_vector != Vector2.ZERO:
		velocity += input_vector * ACC * delta / 2
		velocity = velocity.clamped(MAX_SPEED)
		
	velocity = velocity.move_toward(Vector2.ZERO, ACC_FRICTION * delta / 2)
	
	if Input.is_action_just_pressed("Roll") && state == MOVE:
		if input_vector != Vector2.ZERO:
			hurtbox.start_invincible(0.5) # 翻滚无敌时间
			velocity = ROLL_SPEED * input_vector
			state = ROLL
	elif Input.is_action_just_pressed("Attack"):
		state = ATTACK
		
	velocity = move_and_slide(velocity) # X = v * t + 1/2 * a * t^2
	
# Idle / Run 状态机
func move_state():
	if input_vector != Vector2.ZERO:
		animationTree.set("parameters/Idle/blend_position", input_vector)
		animationTree.set("parameters/Run/blend_position", input_vector)
		animationTree.set("parameters/Roll/blend_position", input_vector)
		animationTree.set("parameters/Attack/blend_position", input_vector)
		animationState.travel("Run")
	else:
		animationState.travel("Idle")
	
func roll_state():
	animationState.travel("Roll")
	
# Attack 状态机
func attack_state():
	animationState.travel("Attack")
	state = DEFAULT
	
# 需要完整播放的动画，在动画末尾帧返回的信号
func complete_animation_finished():
	velocity = velocity * 0.7
	state = MOVE

func _on_Hurtbox_area_entered(area):
	stat.health -= area.damage
	
	var hitEffect = HitEffect.instance()
	get_parent().add_child(hitEffect)
	hitEffect.global_position = global_position
	
	hurtbox.start_invincible(1)
