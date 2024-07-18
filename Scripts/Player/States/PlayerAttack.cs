using Godot;
using System;

public partial class PlayerAttack : State
{
	// Timed variables
	public override bool TimedState { get; set; } = true;
	public override float MinTimeInState { get; set; } = 0.5f;
	public override float MaxTimeInState { get; set; } = 0.5f;
	public override float TimeInState { get; set; } = 0.0f;
	
	// Nodes
	protected Player Player { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }
	protected Area2D Sword { get; private set; }
	protected CollisionShape2D AttackArea { get; private set; }

	public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Sword = Player.GetNode<Area2D>("Sword");
		AttackArea = Sword.GetNode<CollisionShape2D>("AttackArea");
	}

	public override void Enter()
	{   
		// Logging
		GD.Print("Entering attack state");

		// Reset timer
		TimeInState = 0.0f;

		// Allow for slight movement while attacking
		Player._velocity.X = Player._velocity.X / 10;

		// Attack animation
		AnimatedSprite.Play("attack");

		// Open Attack hitbox
		AttackArea.Disabled = false;
	}

	public override void Exit()
	{
		GD.Print("Exiting attack state");
		AnimatedSprite.Stop();
		AttackArea.Disabled = true;
	}

	public override void Update(double delta)
	{
		// Increment State Timer
		TimeInState += (float)delta;

		// Flip hitbox if going left
		FlipHitBox();

		// Can transition if min time is met
		if (TimeInState >= MinTimeInState)
		{
			if (!Player.IsOnFloor())
			{
				fsm.TransitionTo("PlayerFall");
			}
			else
			{
				fsm.TransitionTo("PlayerIdle");
			}
		}
	}

	public override void PhysicsUpdate(double delta)
	{
		// Gravity, Velocity, MoveandSlide
		GravityForce(delta);
		Player.Velocity = Player._velocity;
		Player.MoveAndSlide();
	}

	public void FlipHitBox()
	{
		if (AnimatedSprite.FlipH)
		{
			AttackArea.Position = new Vector2(-12.5f, AttackArea.Position.Y);
		}
		else
		{
			AttackArea.Position = new Vector2(12.5f, AttackArea.Position.Y);
		}
	}
	
	public void GravityForce(double delta)
	{
		if (!Player.IsOnFloor())
		{
			Player._velocity.Y += Player.Gravity * (float)delta;
		}
	}
}
