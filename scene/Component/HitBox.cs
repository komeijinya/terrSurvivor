using Godot;
using System;
using System.Collections.Generic;


public partial class HitBox : Area2D
{
	[Signal] public delegate void HitEventHandler();

	[Export] public float KnockBackStrength = 30;

	private List<HurtBox> hurtBoxes = new List<HurtBox>();
	public float Damage = 10;
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		AreaExited += OnAreaExited;
	}

	public override void _Process(double delta)
	{
		if (hurtBoxes != null)
		{
			foreach (HurtBox hurtbox in hurtBoxes)
			{
				hurtbox.Hurting(this);
				EmitSignal(SignalName.Hit);
			}

		}


	}

	public void OnAreaEntered(Area2D area)
	{
		hurtBoxes.Add((HurtBox)area);
		
	}

	public void OnAreaExited(Area2D area)
    {
		hurtBoxes.Remove((HurtBox)area);
    }

}
