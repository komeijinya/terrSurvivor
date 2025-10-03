using Godot;
using System;
using System.Runtime.InteropServices;

public partial class Sword : CharacterBody2D
{
	public float BaseDamage = 20;
	public HitBox HitBox;

	public AnimationPlayer AnimationPlayer;

	private Vector2 offset = Vector2.Zero;
	public override void _Ready()
	{
		HitBox = GetNode<HitBox>("RotationNode/HitBox");
		HitBox.Damage = BaseDamage;
		Player player = GetTree().GetFirstNodeInGroup("Player") as Player;
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		if (player.AnimatedSprite2D.FlipH == true)
		{
			AnimationPlayer.Play("rightslash");
		}
		else if(player.AnimatedSprite2D.FlipH == false)
		{
			AnimationPlayer.Play("leftslash");
		}

		offset = SetOffset();
	}

	public Vector2 SetOffset()
	{
		Player player = GetTree().GetFirstNodeInGroup("Player") as Player;
		if (player.AnimatedSprite2D.FlipH == true)
		{
			return Vector2.Right * 20;
		}
		else if(player.AnimatedSprite2D.FlipH == false)
		{
			return Vector2.Left * 20;
		}
		return Vector2.Zero;
	}
	public override void _Process(double delta)
	{
		GlobalPosition = (GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition + offset;
	}

	
}
