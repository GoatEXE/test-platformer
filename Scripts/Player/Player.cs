using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Movement
	public Vector2 _velocity = Vector2.Zero;
	public float Speed = 300.0f;
	public float JumpVelocity = -400.0f;

	// Environment
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Objects
	public StateMachine fsm;

	public override void _Ready()
	{
		fsm = GetNode<StateMachine>("StateMachine");
	}

	public override void _Process(double delta)
	{
		Vector2 direction = Input.GetVector("left", "right", "up", "down");

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			fsm.TransitionTo("PlayerJump");
		}
		
		else if (direction != Vector2.Zero && IsOnFloor())
		{
			fsm.TransitionTo("PlayerMove");
		}

		else if (!IsOnFloor() && Velocity.Y >= 0)
		{
			fsm.TransitionTo("PlayerFall");
		}

		else if (IsOnFloor() && fsm._currentState.Name != "PlayerJump")
		{
			fsm.TransitionTo("PlayerIdle");
		}
	}

	public Vector2 GetVelocityInProcess()
	{
		return _velocity;
	}








	// // Animation
	// private AnimatedSprite2D _animatedSprite;

	// private StateMachine _stateMachine;
	// private IdleState _idleState;

	// public override void _Ready()
	// {
	// 	base._Ready();

	// 	_stateMachine = new StateMachine();
	// 	_idleState = new IdleState();

	// 	_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	// }

	// public override void _PhysicsProcess(double delta)
	// {
	// 	HandleMovement();
	// 	HandleJump();
	// 	UpdateAnimation(); 

	// 	// Velocity = _velocity;
	// 	// MoveAndSlide();
	// }

	// private void HandleMovement()
	// {
	// 	Vector2 direction = Input.GetVector("left", "right", "up", "down");

	// 	if (direction != Vector2.Zero)
	// 	{
	// 		_velocity.X = direction.X * Speed;
	// 	}
	// 	else
	// 	{
	// 		_velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
	// 	}
	// }

	// private void HandleJump()
	// {
	// 	if (Input.IsActionJustPressed("jump") && IsOnFloor())
	// 	{
	// 		_velocity.Y = JumpVelocity;
	// 	}
	// }

	// private void UpdateAnimation()
	// {
	// 	// Update the animation based on the player's state
	// 	if (IsOnFloor())
	// 	{
	// 		if (_velocity.X == 0)
	// 		{
	// 			_animatedSprite.Play("idle");
	// 		}
	// 		else
	// 		{
	// 			_animatedSprite.Play("walk");
	// 		}
	// 	}
	// 	else
	// 	{
	// 		_animatedSprite.Play("jump");
	// 	}

	// 	// Horizontal flip based on direction moved
	// 	if (_velocity.X > 0)
	// 	{
	// 		_animatedSprite.FlipH = false;
	// 	}
	// 	else if (_velocity.X < 0)
	// 	{
	// 		_animatedSprite.FlipH = true;
	// 	}
	// }

}
