### Summary: A node class for actor health

extends actorNode

class_name actorStatHealth_class

@export var actorStatHealthMaxBase : int
@export var actorStatHealthCurrent : int
var actorStatHealthMaxModified : int

func _ready():
	# Calculate max base health with any game stat buffs to get the currrent health
	actorStatHealthCurrent = actorStatHealthMaxModified
