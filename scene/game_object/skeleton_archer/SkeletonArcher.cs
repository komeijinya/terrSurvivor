using Godot;
using System;

public partial class SkeletonArcher : BasicEnemy
{

	private MoveComponent moveComponent;
	private HurtBox hurtBox;
	private HealthComponent healthComponent;

	private ShootComponent shootComponent;
	[Export] public float ShootDistance = 350.0f;
	[Export] public float RetreatDistance = 50.0f;

	bool isShooting = false;
	private enum State { Chase, Aim, Retreat }

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
		shootComponent = GetNode<ShootComponent>("ShootComponent");
		shootComponent.Shooted += OnShooted;
		currentState = State.Chase;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		

		float distanceToPlayer = GlobalPosition.DistanceTo((GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition);

		switch (currentState)
		{
			case State.Chase:
				shootComponent.Stop();
				if (distanceToPlayer <= ShootDistance)
				{
					currentState = State.Aim;
				}

				else if (distanceToPlayer <= RetreatDistance)
				{
					currentState = State.Retreat;
				}
				else
				{
					animatedSprite2D.FlipH = GetDirectionToPlayer().X > 0 ? true : false;
					animatedSprite2D.Play("Walk");
					moveComponent.AccelerateInDirection(GetDirectionToPlayer());
					moveComponent.Move(this);
				}
				break;
			case State.Aim:
				animatedSprite2D.Play("Shoot");
				animatedSprite2D.FlipH = GetDirectionToPlayer().X > 0 ? true : false;
				if (!isShooting)
				{
					shoot();
				}

				if (distanceToPlayer > ShootDistance)
				{
					currentState = State.Chase;
				}
				else if (distanceToPlayer <= RetreatDistance)
				{
					currentState = State.Retreat;
				}

				break;
			case State.Retreat:
				animatedSprite2D.FlipH = -GetDirectionToPlayer().X > 0 ? true : false;
				animatedSprite2D.Play("Walk");
				moveComponent.AccelerateInDirection(-GetDirectionToPlayer());
				moveComponent.Move(this);
				if(distanceToPlayer >= RetreatDistance)
				{
					currentState = State.Chase;
				}
				break;
		}


	}

	public void OnDied()
	{
		QueueFree();
	}

	public void shoot()
	{
		shootComponent.shoot();
		isShooting = true;
	}

	public void OnShooted()
	{
		isShooting = false;
	}
}
