extends Node2D

func create_grass_effect():
	var GrassEffect = load("res://Effects/GrassEffect.tscn")  # 类声明
	var grassEffect = GrassEffect.instance()                  # 实例化
	var world = get_tree().current_scene
	world.add_child(grassEffect)
	grassEffect.global_position = global_position
	
func _on_Hurtbox_area_entered(area):
	create_grass_effect()
	queue_free()
