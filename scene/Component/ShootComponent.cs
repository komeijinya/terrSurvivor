using Godot;
using System;

public partial class ShootComponent : Node2D
{
    [Export] PackedScene Projectile;

    [Signal] public delegate void ShootedEventHandler();

    float BaseRadius = 0;

    bool isExplosion = false;

    public Vector2 Direction = Vector2.Right;

    Timer timer;

    public bool CurrentShoot = false;


    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimeOut;
    }

    public void OnTimeOut()
    {
        BasicProjectile ProjectileInstance = Projectile.Instantiate<BasicProjectile>();
        if(isExplosion)
        {
            ProjectileInstance.radius = BaseRadius;
        }
        if(!ProjectileInstance.IsFriend)
        {
            Direction = GetDirectionToPlayer();
        }
        ProjectileInstance.Direction = Direction;
        ProjectileInstance.GlobalPosition = GlobalPosition;
        ProjectileInstance.LookAt(ProjectileInstance.GlobalPosition + ProjectileInstance.Direction);
        GetTree().GetFirstNodeInGroup("Projectile").AddChild(ProjectileInstance);
        EmitSignal(SignalName.Shooted);
        
    }
	public Vector2 GetDirectionToPlayer()
	{
		Vector2 Direction = ((GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition - GlobalPosition).Normalized();
		return Direction;
	}

    public void shoot()
    {
        if(!CurrentShoot)
        {
            timer.Start();
        }
        else
        {
            OnTimeOut();
        }
        
    }
    public void Stop()
    {
        timer.Stop();
        EmitSignal(SignalName.Shooted);
    }

    public void SetCurrent()
    {
        CurrentShoot = true;
    }

    public void SetExplosion(float Radius)
    {
        isExplosion = true;
        BaseRadius = Radius;
    }

}
