using Godot;
using System;

public partial class SkeletonArcher : BasicEnemy
{
	
	private MoveComponent moveComponent;
	private HurtBox hurtBox;
	private HealthComponent healthComponent;
    [Export] public float ShootDistance = 250.0f;
    [Export] public float RetreatDistance = 50.0f;
	private enum State {Chase,Aim,Retreat}

	private State currentState;

	private AnimatedSprite2D animatedSprite2D;
	public override void _Ready()
	{
		moveComponent = GetNode<MoveComponent>("MoveComponent");
		moveComponent.MaxSpeed = MaxSpeed;
		moveComponent.acceleration = Acceleration;
		healthComponent = GetNode<HealthComponent>("HealthComponent");
		healthComponent.MaxHealth = MaxHealth;
		healthComponent.Died += OnDied;
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		animatedSprite2D.FlipH = GetDirectionToPlayer().X > 0 ? true : false;

		
		moveComponent.AccelerateInDirection(GetDirectionToPlayer());
		moveComponent.Move(this);
	}

	public void OnDied()
	{
		QueueFree();
	}
}
