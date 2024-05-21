extends Area2D

@export var expValue : int = 1


func _on_body_entered(body):
	print("coin get! EXP increased")
	
	$"../../Minra_Starlight_Player_Prefab".addEXP(expValue)
	
	queue_free()
