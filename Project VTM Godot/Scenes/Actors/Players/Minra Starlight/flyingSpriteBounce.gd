extends AnimatedSprite2D

# Properties
@export var amplitude = 100  # Amplitude of the sine wave
@export var speed = 100     # Speed of movement along the wave

var phase_shift = 0         # Phase shift of the sine wave
var original_position = Vector2.ZERO

func _ready():
	original_position = position

func _process(delta):
	var offset = (amplitude * sin(phase_shift) - amplitude/2)
	var new_position = (original_position + Vector2(0, offset))
	
	position.y = new_position.y
	
	# Update phase shift for next frame
	phase_shift += speed * delta
