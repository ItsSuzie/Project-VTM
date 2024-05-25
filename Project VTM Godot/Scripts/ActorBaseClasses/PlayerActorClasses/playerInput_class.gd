extends actorNode

class_name playerInput_class

func playerInputProcessing():
	#Gets player input as a float, X and Y movement
	var x_mov = Input.get_action_strength("move_right") - Input.get_action_strength("move_left")
	var y_mov = Input.get_action_strength("move_down") - Input.get_action_strength("move_up")
	var mov = Vector2(x_mov, y_mov) # apples it to a vector2
	
	return mov

# handles player input
func _input(event):

	if event.is_action_pressed("skill_1"):
		print("Skill 1 activated")
		$"../skillLabel".modulate = Color.RED
	if event.is_action_pressed("skill_2"):
		print("Skill 2 activated")
		$"../skillLabel".modulate = Color.GREEN
	if event.is_action_pressed("skill_3"):
		print("Skill 3 activated")
		$"../skillLabel".modulate = Color.BLUE
	if event.is_action_pressed("super"):
		print("super activated")
		$"../skillLabel".modulate = Color.PURPLE
		
	
