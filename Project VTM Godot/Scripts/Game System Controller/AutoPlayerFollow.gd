extends Node2D

# What object to connect to to follow the player
@export var playerTarget : CharacterBody2D = null


# Called when the node enters the scene tree for the first time.
func _ready():
	
	### TODO: Instead of every frame following the player through code, use code here to
	### automatically find, and attach as a child of the player
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	position = playerTarget.position
