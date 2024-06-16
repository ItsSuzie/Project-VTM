extends CharacterBody2D
class_name pickupCoinEXPBase

# Get the player node
@onready var player = $"../../Minra_Starlight_Player_Prefab"

# pickup type
enum pickupTypeENUM {COIN, EXP}
@export var pickupType : pickupTypeENUM

# pickup value
@export var expValue : int = 1
@export var coinValue : int = 1

# EXP Movement speed
@export var movement_speed = 142.0;
var no_move = true;


# TODO: Create cool EXP bit object, I'm thinking a modified twitch bit with the pioints flatted off


# If the coin has entered the player's pickup radius, the coin can now move
func _on_xp_body_entered(body):
	if body.name == "Player":
		no_move = false;



# If it has entered the player body
func _on_body_entered(body):
	if pickupType == pickupTypeENUM.COIN:
		print("coin get! Added coin cound")
		body.addEXP(expValue)
		
	if pickupType == pickupTypeENUM.EXP:
		print("EXP Bit get! Add exp")
		body.addEXP(expValue)
	
	queue_free()



func _process(_delta):
	if !no_move && !str(player)=="[Deleted Object]":
		var direction = global_position.direction_to(player.global_position);
		var velocity = direction*movement_speed;
		move_and_slide();
		
		if direction.x > 0.1:
			$sprite_xp.flip_h = true;
		elif direction.x < -0.1:
			$sprite_xp.flip_h = false;



func _on_delete_body_entered(body):
	if body.has_method("on_gold"):
		body.on_gold(expValue);
		queue_free()
		


