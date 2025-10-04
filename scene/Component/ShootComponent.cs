using Godot;
using System;

public partial class ShootComponent : Node2D
{
    [Export] PackedScene Projectile;

    Timer timer;


    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimeOut;
    }

    public void OnTimeOut()
    {
        SkeletonArrow ProjectileInstance = Projectile.Instantiate<SkeletonArrow>();
        ProjectileInstance.Direction = GetDirectionToPlayer();
        ProjectileInstance.GlobalPosition = GlobalPosition;
        ProjectileInstance.LookAt(ProjectileInstance.GlobalPosition + ProjectileInstance.Direction);
         GetTree().GetFirstNodeInGroup("Projectile").AddChild(ProjectileInstance);
        
    }
	public Vector2 GetDirectionToPlayer()
	{
		Vector2 Direction = ((GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition - GlobalPosition).Normalized();
		return Direction;
	}



}
