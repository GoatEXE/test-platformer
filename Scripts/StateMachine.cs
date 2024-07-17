using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	[Export]
	public NodePath InitialState;
	public State _currentState;
	private Dictionary<string, State> _states;

	public override void _Ready()
	{
		_states = new Dictionary<string, State>();
		foreach (Node node in GetChildren())
		{
			if (node is State state)
			{
				_states[node.Name] = state;
				state.fsm = this;
			}
		}

		_currentState = GetNode<State>(InitialState);
		_currentState.Enter();
	}

	public override void _Process(double delta)
	{
		_currentState.Update((float) delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentState.PhysicsUpdate((float) delta);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		_currentState.HandleInput(@event);
	}

	public void TransitionTo(string key)
	{
		if (!_states.ContainsKey(key) || _currentState == _states[key])
		{
			return;
		}

		if (_currentState.TimedState == true)
		{
			if (_currentState.TimeInState < _currentState.MinTimeInState)
			{
				return;
			}
		}


		_currentState.Exit();
		_currentState = _states[key];
		GD.Print(DateTime.Now.ToString("HH:mm:ss.fff") + " - Transitioning to state: " + _currentState.Name);
		_currentState.Enter();
	}
}
