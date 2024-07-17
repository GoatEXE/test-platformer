using Godot;
using System;

public partial class PlayerJump : State
{
	protected Player Player { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }

	public override bool TimedState { get; set; } = true;
	public override float MinTimeInState { get; set; } = 0.1f;
	public override float MaxTimeInState { get; set; } = 0.25f;
	public override float TimeInState { get; set; } = 0.0f;

	public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void Enter()
	{
		GD.Print("Entering Jump state.");
		AnimatedSprite.Play("jump");
		TimeInState = 0.0f; 
	}

	public override void Exit()
	{
		GD.Print("Exiting Jump state.");
		AnimatedSprite.Stop();       
	}

	public override void Update(double delta)
	{
		AnimatedSprite.FlipH = Player.Velocity.X < 0;
	}
	
	public override void PhysicsUpdate(double delta)
	{
		// Apply Gravity
		GravityForce(delta);

		if (TimeInState < MaxTimeInState)
		{
			// Apply Jump Velocity
			Player._velocity.Y = Player.JumpVelocity;
			TimeInState += (float)delta;
		}
		else if (Player._velocity.Y < 0)
		{
			Player._velocity.Y = 0;
		}

		// Left/right direction
   		var input = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		
		if (input != 0)
		{
			Player._velocity.X = Player.Speed * input;
			Player._velocity.X = Mathf.Clamp(Player._velocity.X, -Player.Speed, Player.Speed);
		}
		else
		{
			Player._velocity.X = 0;
		}

		// Apply velocity and move
		Player.Velocity = Player._velocity;
		Player.MoveAndSlide();

	}

	public void GravityForce(double delta)
	{
		if (!Player.IsOnFloor())
		{
			Player._velocity.Y += Player.Gravity * (float)delta;
		}
	}
}
