extends actorNode

class_name actorStatCritRate_class

### Summary: A node class for actor health

@export var actorStatCritRateMaxBase : int
@export var actorStatCritRateCurrent : int
var actorStatCritRatekMaxModified : int

func _ready():
	# Calculate max base move speed with any game stat buffs to get the current move speed
	actorStatCritRateCurrent = actorStatCritRatekMaxModified
