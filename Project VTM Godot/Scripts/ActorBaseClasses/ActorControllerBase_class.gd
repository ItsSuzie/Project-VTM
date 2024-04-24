extends CharacterBody2D
class_name actorControllerBase

### Summary:
### Baseclass for actor controling 
###

### Variable - Private to this class only
var moveSpeed : int # Player movement speed 
var mov	: Vector2	# to store the movment vector of the actor

### Store references to the nodes used on this actor
# input
var playerInputNode : Node = null
var enemyAINode : Node = null 
var friendlyAINode : Node = null

# stats
var statAttackNode : Node = null
var statCritrateNode : Node = null
var statHasteNode : Node = null
var statHealthNode : Node = null
var statMoveSpeedNode : Node = null
var statPickupRadNode : Node = null

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
	


func _process(_delta):	
	
	# Checks to see if one of the input nodes is avaliable. If it is, start processing
	# it's movement values to move this actor.
	
	# If an input class exists, continue to the processing
	if playerInputNode and enemyAINode and friendlyAINode == null:
		printerr("No input related node attached to this actor. Please add either an player input, enemy AI or Friendly AI node to this.")
	else:
		if playerInputNode != null:
			mov = $playerInput_class.playerInputProcessing()
			updateActorGraphics()
		
		if enemyAINode != null:
			mov = $enemyAIActor_class.enemyAIProcessing()
			updateActorGraphics()
			
		if friendlyAINode != null:
			mov = $friendlyAIActor_class.friendlyAIProcessing()
			updateActorGraphics()

		


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

### Handles movement of the actor
func actorMovement(delta):
	
	
	if $actorStatMoveSpeed_class != null:
		moveSpeed = $actorStatMoveSpeed_class.MoveSpeedCurrent
		var calculatedMoveSpeed = (moveSpeed * delta) * 100
		#print(str(calculatedMoveSpeed))
		
		velocity = mov.normalized() * calculatedMoveSpeed
		move_and_slide()
	else:
		print("Node 'actorStatMoveSpeed_Class' missing from this actor. Cannot move.")
		print("Please add this class to allow the actor to move.")
