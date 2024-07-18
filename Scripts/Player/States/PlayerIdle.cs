using Godot;
using System;

public partial class PlayerIdle : State
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
		GD.Print("Entering Idle state.");
		AnimatedSprite.Play("idle");

		Player._velocity = Vector2.Zero;
	}

	public override void Exit()
	{
		GD.Print("Exiting Idle state.");
		AnimatedSprite.Stop();
	}

	public override void Update(double delta)
	{
		Vector2 direction = Input.GetVector("left", "right", "up", "down");

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

		// Fall if floor is removed from player
		if (!Player.IsOnFloor())
		{
			fsm.TransitionTo("PlayerFall");
		}

		// Move State
		if (direction != Vector2.Zero)
		{
			fsm.TransitionTo("PlayerMove");
		}
	}
}
