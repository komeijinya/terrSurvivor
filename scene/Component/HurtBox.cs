using Godot;
using System;

public partial class HurtBox : Area2D
{
	[Signal] public delegate void HurtEventHandler();

	[Export] HealthComponent HealthComponent;

	[Export] bool isKnockable = false;

	[Export] MoveComponent moveComponent;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

	public void OnAreaEntered(Area2D area)
	{
		if (area is not HitBox)
		{
			return;
		}
		HitBox hitarea = (HitBox)area;
		HealthComponent.Damage(hitarea.Damage);
		if(isKnockable)
		{
			KnockingBack(area as HitBox);
		}
		EmitSignal(SignalName.Hurt);
		
	}
	public void KnockingBack(HitBox hitBox)
	{
		Vector2 KnockDirection = (GlobalPosition - hitBox.GlobalPosition).Normalized();
		moveComponent.GetPush(KnockDirection,hitBox.KnockBackStrength);
	}

}
