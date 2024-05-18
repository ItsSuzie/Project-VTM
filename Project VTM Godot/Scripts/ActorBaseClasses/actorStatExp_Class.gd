extends actorNode

class_name actorStatExp_class

var currentLevel : int = 1 	#Stores the player's current level
var currentEXP : int = 0	#stores current exp count 

# Controls the player leveling up
func levelUp():
	currentLevel += 1
	currentEXP = 0
	$CurrentLevel.text = "Level: " + str(currentLevel)

func addEXP(expCount):
	currentEXP += expCount
	$CurrentEXP.text = "EXP: " + str(currentEXP)
	# EXP Check
	if currentEXP >= 20:
		levelUp()
