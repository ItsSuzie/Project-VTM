extends CharacterBody2D
class_name actorControllerBase

### Summary:
### Baseclass for actor controling 
###

### Variable - Private to this class only
var moveSpeed : int # Player movement speed 
var mov : Vector2	# to store the movment vector of the actor

### Store references to the nodes used on this actor
# input
@onready var playerInputNode : Node = null
@onready var enemyAINode : Node = null 
@onready var friendlyAINode : Node = null

# stats
@onready var statAttackNode : Node = null
@onready var statCritrateNode : Node = null
@onready var statHasteNode : Node = null
@onready var statHealthNode : Node = null
@onready var statMoveSpeedNode : Node = null
@onready var statPickupRadNode : Node = null
@onready var statEXPNode : Node = null


### Functions:

# Called when the node enters the scene tree for the first time.
func _ready():
	# reference to input relation nodes
	playerInputNode = $playerInput_class
	enemyAINode = $enemyAIActor_class
	friendlyAINode = $friendlyAIActor_class
	
	# reference to stat nodes
	statAttackNode = $actorStatAttack_class
	statCritrateNode = $actorStatCritRate_class
	statHasteNode = $actorStatHaste_class
	statHealthNode = $actorStatHealth_class
	statMoveSpeedNode = $actorStatMoveSpeed_class
	statPickupRadNode = $actorStatPickupRadius
	statEXPNode = $actorStatExp_class



func _process(_delta):	
	
	# Checks to see if one of the input nodes is avaliable. If it is, start processing
	# it's movement values to move this actor.
	
	# If an input class exists, continue to the processing
	if playerInputNode and enemyAINode and friendlyAINode == null:
		printerr("No input related node attached to this actor. Please add either an player input, enemy AI or Friendly AI node to this.")
	else:
		if playerInputNode != null:
			mov = playerInputNode.playerInputProcessing()
			updateUIGraphics()
		
		if enemyAINode != null:
			mov = enemyAINode.enemyAIProcessing()
			
		if friendlyAINode != null:
			mov = friendlyAINode.friendlyAIProcessing()
		
		updateActorGraphics()


# Adds experience to the player
func addEXP(expAmount):
	statEXPNode.addEXP(expAmount)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	# move the actor
	actorMovement(delta)

### Updates actor graphics
func updateActorGraphics():
	if mov.x < 0:
		$AnimatedSprite2D.flip_h = true;
	elif mov.x > 0:
		$AnimatedSprite2D.flip_h = false;

### Update any and all ui graphics
func updateUIGraphics():
	$Healthlabel.text = "Health: " + str(statHealthNode.actorStatHealthCurrent)

### Handles movement of the actor
func actorMovement(delta):
	if statMoveSpeedNode != null:
		moveSpeed = statMoveSpeedNode.MoveSpeedCurrent
		var calculatedMoveSpeed = (moveSpeed * delta) * 100
		#print(str(calculatedMoveSpeed))
		
		velocity = mov.normalized() * calculatedMoveSpeed
		move_and_slide()
	else:
		print("Node 'actorStatMoveSpeed_Class' missing from this actor. Cannot move.")
		print("Please add this class to allow the actor to move.")
