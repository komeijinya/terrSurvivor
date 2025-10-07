using Godot;
using System;

public partial class BasicProjectile : CharacterBody2D
{
	[Export] public bool IsFriend = false;
	[Export] public int Penetrate = 1;
	public Vector2 Direction = Vector2.Right;
	[Export] public float Damage = 10;

	[Export] public float Speed = 600f;
	[Export] public float acceleration = 50;

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

	public void OnTimeOut()
	{
		QueueFree();
	}

	public void OnHit()
	{
		Penetrate -= 1;
		if ( Penetrate <= 0 )
		{
			QueueFree();
		}
	}



}
