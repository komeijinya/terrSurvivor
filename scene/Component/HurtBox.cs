using Godot;
using System;

public partial class HurtBox : Area2D
{
	[Signal] public delegate void HurtEventHandler();

	[Export] HealthComponent HealthComponent;

	[Export] bool isKnockable = false;

	[Export] MoveComponent moveComponent;

	private Timer hurtInterval;

	private bool CanHurt = true;

	public override void _Ready()
	{
		hurtInterval = GetNode<Timer>("Timer");
		AreaEntered += OnAreaEntered;
		hurtInterval.Timeout += OnHurtIntervalTimeOut;
	}

	public void OnAreaEntered(Area2D area)
	{
		// if (area is not HitBox)
		// {
		// 	return;
		// }
		// HitBox hitarea = (HitBox)area;
		// HealthComponent.Damage(hitarea.Damage);
		// if (isKnockable)
		// {
		// 	KnockingBack(area as HitBox);
		// }
		// EmitSignal(SignalName.Hurt);

	}

	public void Hurting(Area2D area)
	{
		GD.Print("hello");
		if (CanHurt)
		{
			if (area is not HitBox)
			{
				return;
			}
			HitBox hitarea = (HitBox)area;
			HealthComponent.Damage(hitarea.Damage);
			if (isKnockable)
			{
				KnockingBack(area as HitBox);
			}
			EmitSignal(SignalName.Hurt);
			CanHurt = false;
			hurtInterval.Start();
		}


	}
	public void KnockingBack(HitBox hitBox)
	{
		Vector2 KnockDirection = (GlobalPosition - hitBox.GlobalPosition).Normalized();
		moveComponent.GetPush(KnockDirection, hitBox.KnockBackStrength);
	}

	public void OnHurtIntervalTimeOut()
    {
		CanHurt = true;
    }

}
