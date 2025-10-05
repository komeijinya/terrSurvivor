using Godot;
using System;
using System.Runtime.InteropServices;

public partial class Sword : CharacterBody2D
{
	public float BaseDamage = 20;
	public HitBox HitBox;

	public AnimationPlayer AnimationPlayer;

	float slashDistance = 30f;

	Vector2 currentDirection;


	public Vector2 Direction = Vector2.Right;
	public override void _Ready()
	{
		HitBox = GetNode<HitBox>("RotationNode/HitBox");
		HitBox.Damage = BaseDamage;
		Player player = GetTree().GetFirstNodeInGroup("Player") as Player;
		Tween tween = CreateTween();
		if(Direction.X >= 0)
		{
			tween.TweenMethod(new Callable(this,MethodName.TweenSlash),-0.15,0.15,0.5).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Quart);
		}
		else
		{
			tween.TweenMethod(new Callable(this,MethodName.TweenSlash),0.15,-0.15,0.5).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Quart);
		}

		tween.TweenCallback(new Callable(this,MethodName.QueueFree));

	}

	public override void _Process(double delta)
	{
		this.LookAt(GlobalPosition + currentDirection);
	}
	
	public void TweenSlash(float rotation)
	{
		Player player = GetTree().GetFirstNodeInGroup("Player") as Player;
		if (player == null)
		{
			return;
		}

		currentDirection = Direction.Rotated(rotation * float.Tau);
		GlobalPosition = player.GlobalPosition + currentDirection * slashDistance;
		


	}
	
}
