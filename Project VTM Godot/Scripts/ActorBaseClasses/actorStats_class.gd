extends Node2D	

class_name actorStats

# variables - current working stats, can be changed mid-game
var actorMaxHealth : int		# The actors max health
var actorCurrentHealth : int	# Actor current health

var actorStatAttack : int		# actor attack stat - for auto attack and weapons/skills
var actorStatMoveSpeed : int	# actor movement speed
var actorStatCritrate : int		# actor crit rate, will mathematically added to the attack stat
var actorStatPickupRadius : int	# actor pickup radius, how far the player can pick up items on the ground
var actorStatHaste : int		# actor auto attack rate, how many times the player can auto attack per second


# original base stats - applied all in _ready()
var actorBaseMaxHealth : int
var actorBaseCurrentHealth : int

var actorBaseStatAttack : int
var actorBaseStatMoveSpeed : int
var actorBaseStatCritrate : int
var actorBaseStatPickupRadius : int
var actorBaseStatHaste : int

func _ready():
	
	# stores the original base actors stats
	actorBaseMaxHealth = actorMaxHealth
	actorBaseCurrentHealth = actorCurrentHealth

	actorBaseStatAttack = actorStatAttack
	actorBaseStatMoveSpeed = actorStatMoveSpeed
	actorBaseStatCritrate = actorStatCritrate
	actorBaseStatPickupRadius = actorStatPickupRadius
	actorBaseStatHaste = actorStatHaste
