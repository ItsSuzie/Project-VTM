### Summary: A node class for actor health

extends actorNode

class_name actorStatMoveSpeed_class

@export var MoveSpeedMaxBase : int
var MoveSpeedCurrent : int
var MoveSpeedMaxModified : int

func _ready():
	# TODO: calculatte max modified move speed from base and the stat buffs and store to modified,
	# then apply to current
	MoveSpeedCurrent = MoveSpeedMaxBase
