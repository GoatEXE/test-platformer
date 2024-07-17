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
}
