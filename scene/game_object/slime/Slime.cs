using Godot;
using System;

public partial class Slime : BasicEnemy
{
	private MoveComponent moveComponent;
	private HurtBox hurtBox;
	private HealthComponent healthComponent;

	private AnimatedSprite2D animatedSprite2D;


	private bool isMoving = false;
	public override void _Ready()
	{

		hurtBox = GetNode<HurtBox>("JumpNode/HurtBox");
		hurtBox.Hurt += OnHurt;
		moveComponent = GetNode<MoveComponent>("MoveComponent");
		moveComponent.MaxSpeed = MaxSpeed;
		moveComponent.acceleration = Acceleration;
		healthComponent = GetNode<HealthComponent>("HealthComponent");
		healthComponent.MaxHealth = MaxHealth;
		healthComponent.Died += OnDied;
		animatedSprite2D = GetNode<AnimatedSprite2D>("JumpNode/AnimatedSprite2D");
	}

	public override void _Process(double delta)
	{
		if(isMoving)
		{
			moveComponent.AccelerateInDirection(GetDirectionToPlayer());
			animatedSprite2D.Play("Jump");
		}
		else
		{
			moveComponent.Decelereate();
			animatedSprite2D.Play("Idle");
		}
			
			moveComponent.Move(this);
	}

	public Vector2 GetDirectionToPlayer()
	{
		Vector2 Direction = ((GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition - GlobalPosition).Normalized();
		return Direction;
	}

	public void SetIsMoving(bool isMove)
	{
		isMoving = isMove;
	}

	public void OnHurt()
	{

	}

	public void OnDied()
	{
		QueueFree();
	}
}
