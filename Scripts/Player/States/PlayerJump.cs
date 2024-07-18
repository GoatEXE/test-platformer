using Godot;
using System;

public partial class PlayerJump : State
{
	protected Player Player { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }

	public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void Enter()
	{
		GD.Print("Entering Jump state.");
		AnimatedSprite.Play("jump");

		// Apply Jump Velocity once when entering the jump state
		Player._velocity.Y = Player.JumpVelocity; 
	}

	public override void Exit()
	{
		GD.Print("Exiting Jump state.");
		AnimatedSprite.Stop();       
	}

	public override void Update(double delta)
	{
		// Flip sprite if facing left
		AnimatedSprite.FlipH = Player._velocity.X < 0;

		// Fall state if at jump apex
		if (!Player.IsOnFloor() && Player.Velocity.Y >= 0)
		{
			fsm.TransitionTo("PlayerFall");
		}

		// Attack state
		if (Input.IsActionJustPressed("attack"))
		{
			fsm.TransitionTo("PlayerAttack");
		}
	}
	
	public override void PhysicsUpdate(double delta)
	{
		// Apply Gravity
		GravityForce(delta);
		
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
