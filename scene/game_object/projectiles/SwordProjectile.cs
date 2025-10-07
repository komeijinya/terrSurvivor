using Godot;
using System;

public partial class SwordProjectile : BasicProjectile
{
	// Called when the node enters the scene tree for the first time.
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
		if(IsFriend)
		{
			SetFriend();
		}
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MoveComponent.AccelerateInDirection(Direction);
		MoveComponent.Move(this);
	}

	public void SetFriend()
	{
		HitBox.SetCollisionLayerValue(4,false);
		HitBox.SetCollisionMaskValue(2,false);

		HitBox.SetCollisionLayerValue(3,true);
		HitBox.SetCollisionMaskValue(5,true);
		
	}

}
