using Godot;
using System;

public partial class EndScreen : CanvasLayer
{
	public Button RestartButton,QuitButton;
	public Label Title,Description;
	public override void _Ready()
	{
		GetTree().Paused = true;
		RestartButton = GetNode<Button>("%RestartButton");
		QuitButton = GetNode<Button>("%QuitButton");
		Title = GetNode<Label>("%Title");
		Description = GetNode<Label>("%Description");
		RestartButton.Pressed += OnRestartButtonPressed;
		QuitButton.Pressed += OnQuitButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnRestartButtonPressed()
	{
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://scene/game/main.tscn");
	}

	public void SetDefeat()
	{
		Title.Text = "Defeat!";
		Description.Text = "你输了";
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
