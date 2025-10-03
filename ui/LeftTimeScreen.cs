using Godot;
using System;

public partial class LeftTimeScreen : CanvasLayer
{
	[Export] GameTimeManager gameTimeManager;

	Label label;
	public override void _Ready()
	{
		label = GetNode<Label>("%Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		label.Text =TimeToString(gameTimeManager.GetTime());
	}

	public string TimeToString(float time)
	{
		string min = ((int)time/60).ToString();
		string sceond = ((int)time%60).ToString();
		string strTime = time.ToString("F2");
		return min + ":" + sceond;
	}
}
