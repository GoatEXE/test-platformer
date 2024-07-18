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
}
