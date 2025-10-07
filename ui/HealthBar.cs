using Godot;
using System;

public partial class HealthBar : CanvasLayer
{
	TextureProgressBar textureProgressBar;
	public override void _Ready()
	{
		textureProgressBar = GetNode<TextureProgressBar>("Progress");
	}

	public void SetValue(float percent)
	{
		textureProgressBar.Value = percent * 100;
	}


}
