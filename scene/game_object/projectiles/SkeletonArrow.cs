using Godot;
using System;

public partial class SkeletonArrow : CharacterBody2D
{
	public Vector2 Direction = Vector2.Right;
	public float Damage = 10;

	[Export] public float Speed = 600f;
	public float acceleration = 50;
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MoveComponent.AccelerateInDirection(Direction);
		MoveComponent.Move(this);
	}

	public void OnTimeOut()
	{
		QueueFree();
	}

	public void OnHit()
	{
		QueueFree();
	}
}
