### Summary: A node class for actor health

extends actorNode

class_name actorStatPickupRadius

@export var actorStatPickupRadiusMaxBase : int
@export var actorStatPickupRadiusCurrent : int
var actorStatPickupRadiusMaxModified : int

func _ready():
	# do calculations to get the final modified pickup radius
	actorStatPickupRadiusCurrent = actorStatPickupRadiusMaxModified
