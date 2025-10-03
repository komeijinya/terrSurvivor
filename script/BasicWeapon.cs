using Godot;
using System;

public partial class BasicWeapon : Node2D
{
	[Export] public string ID = "BasicWeapon";

	[Export] public double BaseCD = 3;

	[Export] public float BaseDamage = 10;

	[Export] public Vector2 BaseScale = Vector2.One;
}
