using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D
{
	[Export] public string ID = "basic_enemy";

	[Export] public float Acceleration = 50f;

	
	[Export] public float MaxHealth = 20f;

	[Export] public float MaxSpeed = 130f;
}
