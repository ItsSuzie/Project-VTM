### Summary: A node class for actor health

extends actorNode

class_name actorStatHealth_class

@export var actorStatHealthMaxBase : int
@export var actorStatHealthCurrent : int
var actorStatHealthMaxModified : int

func _ready():
	# Calculate max base health with any game stat buffs to get the currrent health
	actorStatHealthMaxModified = actorStatHealthMaxBase
	actorStatHealthCurrent = actorStatHealthMaxModified

# Takes damage based on how much damage is passed in
func takeDamage(damage):
	actorStatHealthCurrent -= damage
	
	if actorStatHealthCurrent <= 0:
		gameOver()

# once called, the game is over, do gameover stuff
func gameOver():
	print("game over!")


# Update UI Health
func updateUI():
	$Healthlabel.text = "Health:" + str(actorStatHealthCurrent)
