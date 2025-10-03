using Godot;
using System;

public partial class Zombie : BasicEnemy
{
	private MoveComponent moveComponent;
	private HurtBox hurtBox;
	private HealthComponent healthComponent;

	private AnimatedSprite2D animatedSprite2D;

	public override void _Ready()
	{
		hurtBox = GetNode<HurtBox>("HurtBox");
		hurtBox.Hurt += OnHurt;
		moveComponent = GetNode<MoveComponent>("MoveComponent");
		moveComponent.MaxSpeed = MaxSpeed;
		moveComponent.acceleration = Acceleration;
		healthComponent = GetNode<HealthComponent>("HealthComponent");
		healthComponent.MaxHealth = MaxHealth;
		healthComponent.Died += OnDied;
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _Process(double delta)
	{
		animatedSprite2D.FlipH = GetDirectionToPlayer().X > 0 ? true : false;
		moveComponent.AccelerateInDirection(GetDirectionToPlayer());
		moveComponent.Move(this);
	}

	public Vector2 GetDirectionToPlayer()
	{
		Vector2 Direction = ((GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition - GlobalPosition).Normalized();
		return Direction;
	}

	public void OnHurt()
	{
		
	}

	public void OnDied()
	{
		QueueFree();
	}
	


}
