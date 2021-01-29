extends Node2D
export (PackedScene) var Mob
var score
var minSpeed
var maxSpeed

func _ready():
	randomize()
	$Player.hide()

func game_over():
	$Music.stop()
	$DeathSound.play()
	$ScoreTimer.stop()
	$MobTimer.stop()
	$HUD.show_game_over()
	get_tree().call_group("mobs", "queue_free")

func new_game():
	$Music.play()
	score = 0
	$Player.start($StartPosition.position)
	$Player.show()
	$StartTimer.start()
	$HUD.update_score(score)
	$HUD.show_message("Get Ready")

func _on_StartTimer_timeout():
    $MobTimer.start()
    $ScoreTimer.start()

func _on_ScoreTimer_timeout():
	score += 1
	$HUD.update_score(score)


func _on_MobTimer_timeout():
	# Choose a random location on Path2D.
	$MobPath/MobSpawnLocation.offset = randi()
	# Create a Mob instance and add it to the scene.
	var mob = Mob.instance()
	add_child(mob)
	# Set the mob's direction perpendicular to the path direction.
	var direction = $MobPath/MobSpawnLocation.rotation + PI / 2
	# Set the mob's position to a random location.
	mob.position = $MobPath/MobSpawnLocation.position
	# Add some randomness to the direction.
	direction += rand_range(-PI / 4, PI / 4)
	mob.rotation = direction
	# Set the velocity (speed & direction).
	minSpeed = mob.min_speed * (1 + (score / 10) * 0.5)
	maxSpeed = mob.max_speed * (1 + (score / 10) * 0.5)
	mob.linear_velocity = Vector2(rand_range(minSpeed, maxSpeed), 0)
	mob.linear_velocity = mob.linear_velocity.rotated(direction)
