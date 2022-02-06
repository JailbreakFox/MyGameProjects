extends KinematicBody2D

export var ACC = 1000             # 加速度a(运动时不考虑摩擦力)
export var ACC_FRICTION = 200     # 摩擦力加速度at
export var MAX_SPEED = 80        # 最大速度
export var KNOCKBACK_SPEED = 140   # 击退速度

enum {
	IDLE,
	WANDER,
	CHASE
}

const EnermyDeathEffect = preload("res://Effects/EnermyDeathEffect.tscn")
const HitEffect = preload("res://Effects/HitEffect.tscn")

onready var sprite = $AnimatedSprite
onready var stat = $stat
onready var ai = $AI
onready var hurtbox = $Hurtbox
onready var detectbox = $Detectbox

var knockback = Vector2.ZERO
var velocity = Vector2.ZERO
var state = IDLE

func _process(delta):
	move(delta)
	
	# 动画状态机
	match state:
		IDLE:
			sprite.flip_h = velocity.x < 0
		WANDER:
			pass
		CHASE:
			sprite.flip_h = velocity.x < 0
				
# 位置控制
func move(delta):
	knockback = knockback.move_toward(Vector2.ZERO, ACC_FRICTION * delta / 2)
	knockback = move_and_slide(knockback)
	
	if detectbox.is_player_detected():
		var player = detectbox.player
		var direction = (player.global_position - global_position).normalized()
		velocity = ai.chase(delta, velocity, direction, MAX_SPEED, ACC)
	velocity = velocity.move_toward(Vector2.ZERO, ACC_FRICTION * delta / 2)
	velocity = move_and_slide(velocity) # X = v * t + 1/2 * a * t^2
	
func _on_Hurtbox_area_entered(area):
	stat.health -= area.damage
	
	var hitEffect = HitEffect.instance()
	get_parent().add_child(hitEffect)
	hitEffect.global_position = global_position
	
	var hitPos = area.global_position
	var hurtPos = hurtbox.global_position
	knockback = (hurtPos - hitPos).normalized() * KNOCKBACK_SPEED
	
func _on_stat_no_health():
	var enermyDeathEffect = EnermyDeathEffect.instance()
	get_parent().add_child(enermyDeathEffect)
	enermyDeathEffect.global_position = global_position
	queue_free()
