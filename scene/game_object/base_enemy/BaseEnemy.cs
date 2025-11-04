using Godot;
using System;

public partial class BaseEnemy : CharacterBody2D
{
    private MoveComponent moveComponent;
    private HurtBox hurtBox;
    private HealthComponent healthComponent;

    private AnimatedSprite2D animatedSprite2D;

    [Export] public string ID = "basic_enemy";

    [Export] public float Acceleration = 50f;


    [Export] public float MaxHealth = 20f;

    [Export] public float MaxSpeed = 130f;

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

    public virtual void OnHurt()
    {

    }

    public virtual void OnDied()
    {
        QueueFree();
    }


}
