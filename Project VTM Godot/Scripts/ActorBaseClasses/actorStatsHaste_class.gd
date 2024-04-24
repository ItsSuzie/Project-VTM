### Summary: A node class for actor health

extends actorNode

class_name actorStatHaste_class

@export var actorStatHasteMaxBase : int
@export var actorStatHasteCurrent : int
var actorStatHasteMaxModified : int

func _ready():
	# math to calculate max modified haste stat
	actorStatHasteCurrent = actorStatHasteMaxModified
