### Summary: A node class for actor health

extends actorNode

class_name actorStatAttack_class

@export var actorStatAttackMaxBase : int
@export var actorStatAttackCurrent : int
var actorStatAttackMaxModified : int

func _ready():
	# Calculate max base attack with any game stat buffs to get the currrent attack
	actorStatAttackCurrent = actorStatAttackMaxModified
