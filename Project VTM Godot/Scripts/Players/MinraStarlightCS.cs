using Godot;
using System;

public partial class MinraStarlightCS : Area2D
{
	
	// Public Variables	
	[Export] public int playerSpeed { get; set; } = 400; // speed is in pixels per second - AAdding export allows for the value to be edited to be set in the ihnspector
	public Vector2 screenSize; // the size of the game window
	
	// Private Variables
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		screenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		var velocity = Vector2.Zero; // Player's movement vector

		// Get's input
		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}
		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}
		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}
		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		// Takes the current above velocity ahnd multiplies it to speed
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * playerSpeed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}

		// Clamp th player from movement outside the viewport
		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, screenSize.X),
			y: Mathf.Clamp(Position.Y, 0, screenSize.Y)
		);

		// Adjust the position of the player sprite based on direction
		if (velocity.X != 0)
		{
			// animatedSprite2D.Animation = "walk";
			// animatedSprite2D.FlipV = false;
			// see the note below about the bool assighnment
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else if (velocity.Y != 0)
		{
			// animatedSprite2D.Animation = "up";
			// animatedSprite2D.FlipV = velocity.Y > 0;
		}
	}
}
