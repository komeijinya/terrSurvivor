using Godot;
using System;

public partial class Knife : CharacterBody2D
{
	[Export] public float Speed = 600f;
	[Export] public int Penetrate = 1;

	public float acceleration = 50;

	public float Damage = 10;

	public Vector2 Direction = Vector2.Right;

	private MoveComponent MoveComponent;
	private Timer timer;

	HitBox HitBox;

	public override void _Ready()
	{
		HitBox = GetNode<HitBox>("HitBox");
		HitBox.Damage = Damage;
		HitBox.Hit += OnHit;
		MoveComponent = GetNode<MoveComponent>("MoveComponent");
		MoveComponent.acceleration = acceleration;
		MoveComponent.MaxSpeed = Speed;
		timer = GetNode<Timer>("Timer");
		timer.Timeout += OnTimeOut;
		
	}

	
	public override void _Process(double delta)
	{
		
		MoveComponent.AccelerateInDirection(Direction);
		MoveComponent.Move(this);
	}

	public void OnHit()
	{
		Penetrate -= 1;
		if ( Penetrate <= 0 )
		{
			QueueFree();
		}
	}

	public void OnTimeOut()
	{
		QueueFree();
	}
}
