using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D
{
	[Export] public string ID = "basic_enemy";

	[Export] public float Acceleration = 50f;

	
	[Export] public float MaxHealth = 20f;

	[Export] public float MaxSpeed = 130f;

	public Vector2 GetDirectionToPlayer()
	{
		Vector2 Direction = ((GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition - GlobalPosition).Normalized();
		return Direction;
	}
}
