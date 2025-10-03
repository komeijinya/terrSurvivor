using Godot;
using System;

public partial class HitBox : Area2D
{
	[Signal] public delegate void HitEventHandler();

	[Export] public float KnockBackStrength = 30;

	public float Damage = 10;
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	public override void _Process(double delta)
	{
	}

	public void OnAreaEntered(Area2D area)
	{
		EmitSignal(SignalName.Hit);
	}

}
