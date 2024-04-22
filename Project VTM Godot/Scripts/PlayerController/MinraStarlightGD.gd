extends CharacterBody2D

# Variables - Exposed to the inspector
@export var speed = 10000 # Player movement speed

# private variables
var screen_size # size of the game window



# Called when the node enters the scene tree for the first time.
func _ready():
	screen_size = get_viewport().size
	
	print("ready")


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	
	
	 #Gets player input as a float, X and Y movement
	var x_mov = Input.get_action_strength("move_right") - Input.get_action_strength("move_left")
	var y_mov = Input.get_action_strength("move_down") - Input.get_action_strength("move_up")
	var mov = Vector2(x_mov, y_mov) # apples it to a vector2
	
	
	if mov.x < 0:
		$AnimatedSprite2D.flip_h = true;
	else:
		$AnimatedSprite2D.flip_h = false;
	
	velocity = mov.normalized() * speed * delta
	move_and_slide()
	
