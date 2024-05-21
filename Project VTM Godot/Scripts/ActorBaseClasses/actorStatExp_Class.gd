extends actorNode

class_name actorStatExp_class

var currentLevel : int = 1 	#Stores the player's current level
var currentEXP : int = 0	#stores current exp count 

func _onReady():
	levelUp()
	UpdateUI()

# Controls the player leveling up
func levelUp():
	currentLevel += 1
	currentEXP = 0
	
	### TODO: On Level up, increase all stats by level up percentage + buff rate increase, as well as show UI to select what to level up
	
	UpdateUI()

	
func addEXP(expCount):
	currentEXP += expCount
	# EXP Check
	if currentEXP >= 20:
		currentEXP = 0
		levelUp()
	
	UpdateUI()

# Updates the UI Elements to ensure all information is currently up to date
func UpdateUI():
		$"../CurrentLevel".text = "Level: " + str(currentLevel)
		$"../CurrentEXP".text = "EXP: " + str(currentEXP)
