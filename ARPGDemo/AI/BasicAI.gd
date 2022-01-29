extends Node

var velocity_fix = Vector2.ZERO # 反馈的修改速度

# delta        间隔时间
# velocity     当前速度
# direction    与目标之间的向量
# max_speed    最快速度
# acc          加速度
func chase(delta, velocity, direction, max_speed, acc):
	velocity_fix = velocity.move_toward(direction * max_speed, acc * delta / 2)
	return velocity_fix
