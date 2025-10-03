using Godot;
using System;

public partial class Experience : Node2D
{
	private Area2D area2D;
	public override void _Ready()
	{
		area2D = GetNode<Area2D>("Area2D");
		area2D.AreaEntered += OnAreaEntered;
	}

	public override void _Process(double delta)
	{
	}

	public void OnAreaEntered(Area2D area)
	{
		GameEvent.Instance.EmitExpBallCollected(1);
		QueueFree();
	}
}
