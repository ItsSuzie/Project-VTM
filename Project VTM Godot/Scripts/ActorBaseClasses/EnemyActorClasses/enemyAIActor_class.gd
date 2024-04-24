extends actorNode

class_name enemyAIActor_class

func enemyAIProcessing():
	#Gets player input as a float, X and Y movement
	var x_mov = Input.get_action_strength("move_right") - Input.get_action_strength("move_left")
	var y_mov = Input.get_action_strength("move_down") - Input.get_action_strength("move_up")
	var mov = Vector2(x_mov, y_mov) # apples it to a vector2
	
	return mov
