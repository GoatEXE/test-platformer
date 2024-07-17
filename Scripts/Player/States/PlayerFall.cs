using Godot;
using System;

public partial class PlayerFall : State
{
	// Nodes
	protected Player Player { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }

	public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void Enter()
	{
		GD.Print("Entering falling state.");
		AnimatedSprite.Play("fall");
	}

	public override void Exit()
	{
		GD.Print("Exiting falling state.");
		AnimatedSprite.Stop();

		if (Player.IsOnFloor())
		{
			Player._velocity.Y = 0;
			GD.Print(Player.GetVelocityInProcess());
		}
	}

	public override void HandleInput(InputEvent @event)
	{
	}

	public override void Update(double delta)
	{
		AnimatedSprite.FlipH = Player.Velocity.X < 0;
	}
	
	public override void PhysicsUpdate(double delta)
	{
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

		// Gravity, Velocity, MoveandSlide
		GravityForce(delta);
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
