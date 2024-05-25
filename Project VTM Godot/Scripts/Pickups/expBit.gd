extends Area2D
class_name expBit


@export var expValue : int = 1


func _on_body_entered(body):
	print("coin get! EXP increased")
	body.addEXP(expValue)
	
	queue_free()
