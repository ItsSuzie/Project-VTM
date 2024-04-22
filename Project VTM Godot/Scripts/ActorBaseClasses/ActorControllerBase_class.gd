extends CharacterBody2D

class_name actorControllerBase

### Summary:
### Baseclass for actor controling and accessing some important stats
###


### Variables - Exposed to the inspector
@export var actor_Selector := actorSelector.PLAYER

# Actor stats to access
@export var actorMaxHealth : int = 0
@export var actorCurrentHealth : int = 0

@export var actorStatAttack : int = 0
@export var actorStatMoveSpeed : int  = 0
@export var actorStatCritrate : int = 0
@export var actorStatPickupRadius : int = 0
@export var actorStatHaste : int = 0



### Variable - Private to this class only
var moveSpeed : int # Player movement speed 
var x_mov : float 	# Stores X Axis movement
var y_mov : float 	# Stores Y Axis movement
var mov	: Vector2	# to store the movment vector of the actor


### Enums
enum actorSelector {PLAYER, ENEMY}


### Functions:

# Called when the node enters the scene tree for the first time.
func _ready():
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	processInspectorStatChanges()
	
	if actor_Selector == actorSelector.PLAYER: # If player, then we process player input
		playerInputProcessing()
	elif actor_Selector == actorSelector.ENEMY: # if enemy, we process enemy AI
		enemyAIProcessing()
	else:
		print("Actor type not selected. Please select an actor type in the inspector.")
		pass
	
	# move the actor
	actorMovement(delta)

### Processess any stat changes in the inspector
func processInspectorStatChanges():
	$actorStatsNode.actorMaxHealth = actorMaxHealth
	$actorStatsNode.actorCurrentHealth = actorCurrentHealth
	$actorStatsNode.actorStatAttack = actorStatAttack
	$actorStatsNode.actorStatMoveSpeed = actorStatMoveSpeed
	$actorStatsNode.actorStatCritrate = actorStatCritrate
	$actorStatsNode.actorStatPickupRadius = actorStatPickupRadius
	$actorStatsNode.actorStatHaste = actorStatHaste

### Processes the player input - Might need logic to be handled in a separate child node
func playerInputProcessing():
	#Gets player input as a float, X and Y movement
	x_mov = Input.get_action_strength("move_right") - Input.get_action_strength("move_left")
	y_mov = Input.get_action_strength("move_down") - Input.get_action_strength("move_up")
	mov = Vector2(x_mov, y_mov) # apples it to a vector2
	
	
	if mov.x < 0:
		$AnimatedSprite2D.flip_h = true;
	else:
		$AnimatedSprite2D.flip_h = false;

### Proceesses enemy AI - logic might go into a separate child node 
func enemyAIProcessing():
	pass

### Handles movement of the actor
func actorMovement(delta):
	
	moveSpeed = $actorStatsNode.actorStatMoveSpeed
	var calculatedMoveSpeed = (moveSpeed * delta) * 100
	#print(str(calculatedMoveSpeed))
	
	velocity = mov.normalized() * calculatedMoveSpeed
	move_and_slide()
