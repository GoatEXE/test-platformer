using Godot;
using System;

public partial class PlayerMove : State
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
		GD.Print("Entering Move state.");
		AnimatedSprite.Play("walk");
	}

	public override void Exit()
	{
		GD.Print("Exiting Move state.");
		AnimatedSprite.Stop();
	}

	public override void Update(double delta)
	{
		// Flip sprite if going left
		AnimatedSprite.FlipH = Player._velocity.X < 0;

		// Jump State
		if (Input.IsActionJustPressed("jump"))
		{
			fsm.TransitionTo("PlayerJump");
		}

		// Attack State
		if (Input.IsActionJustPressed("attack"))
		{
			fsm.TransitionTo("PlayerAttack");
		}

	}

	public override void PhysicsUpdate(double delta)
	{
		var input = Input.GetActionStrength("right") - Input.GetActionStrength("left");

		if (input != 0)
		{
			Player._velocity.X = Player.Speed * input;
			Player._velocity.X = Mathf.Clamp(Player._velocity.X, -Player.Speed, Player.Speed);
		}
		else
		{
			Player._velocity.X = 0;
			fsm.TransitionTo("PlayerIdle");
		}

		// Velocity and Move
		Player.Velocity = Player._velocity;
		Player.MoveAndSlide();
	}
}
