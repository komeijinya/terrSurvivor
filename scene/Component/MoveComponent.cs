using Godot;
using System;

public partial class MoveComponent : Node
{
    [Export] public float MaxSpeed = 300f;
	[Export] public double acceleration = 50d;
	Vector2 velocity = Vector2.Zero;
    public void AccelerateInDirection(Vector2 direction)
	{
		Vector2 target_velocity = direction * MaxSpeed;
		velocity = velocity.Lerp(target_velocity,(float)(1-System.Math.Exp(-acceleration * GetProcessDeltaTime())));
	}

	public void AccelerateInDirection(Vector2 direction,float strength)
	{
		Vector2 target_velocity = direction * strength;
		velocity = velocity.Lerp(target_velocity,(float)(1-System.Math.Exp(-acceleration * GetProcessDeltaTime())));
	}

    public void Decelereate()
    {
        AccelerateInDirection(Vector2.Zero);
    }

	public void Move(CharacterBody2D body)
	{

		body.Velocity = velocity;
		body.MoveAndSlide();
		velocity = body.Velocity;
	}

	public void GetPush(Vector2 direction,float strength)
	{
		AccelerateInDirection(direction,strength/3 * 100);
		AccelerateInDirection(velocity.Normalized(),strength/3 * 200);
	}
}
