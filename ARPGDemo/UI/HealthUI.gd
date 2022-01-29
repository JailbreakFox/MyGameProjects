extends Control

var hearts = 4 setget set_hearts
var max_hearts = 4 setget set_max_hearts

onready var HeartUIEmpty = $HeartUIEmpty
onready var HeartUIFull = $HeartUIFull

func set_hearts(value):
	hearts = clamp(value, 0, max_hearts)
	if HeartUIFull != null:
		HeartUIFull.rect_size.x = hearts * 15
	
func set_max_hearts(value):
	max_hearts = max(value, 1)
	if HeartUIEmpty != null:
		HeartUIEmpty.rect_size.x = max_hearts * 15

# Called when the node enters the scene tree for the first time.
func _ready():
	self.hearts = PlayerStat.health
	self.max_hearts = PlayerStat.MAX_HEALTH
	PlayerStat.connect("health_changed", self, "set_hearts")
