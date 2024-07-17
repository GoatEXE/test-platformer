using Godot;
using System;

public partial class State : Node
{
    public StateMachine fsm;

    public virtual bool TimedState { get; set; } = false;
    public virtual float MinTimeInState { get; set; }
    public virtual float MaxTimeInState { get; set; }
    public virtual float TimeInState { get; set; }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public override void _Ready()
    {
    }

    public virtual void Update(double delta)
    {
    }

    public virtual void PhysicsUpdate(double delta)
    {
    }

    public virtual void HandleInput(InputEvent @event)
    {
    }
}
