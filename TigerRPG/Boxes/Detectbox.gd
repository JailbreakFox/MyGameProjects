extends Area2D

var player = null

func is_player_detected():
	return player != null

func _on_Detectbox_body_entered(body):
	player = body

func _on_Detectbox_body_exited(body):
	player = null
