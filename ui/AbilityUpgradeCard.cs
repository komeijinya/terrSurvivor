using Godot;
using System;

public partial class AbilityUpgradeCard : PanelContainer
{
	public Label NameLabel;
	public Label DescriptionLabel;

	[Signal] public delegate void SelectedEventHandler();
	public override void _Ready()
	{
		NameLabel = GetNode<Label>("VBoxContainer/NameLabel");
		DescriptionLabel = GetNode<Label>("VBoxContainer/DescriptionLabel");
		GuiInput += OnGuiInput;
	}
	public void SetAbilityUpgrade(ability_upgrade upgrade)
	{
		NameLabel.Text = upgrade.Name;
		DescriptionLabel.Text = upgrade.Description;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnGuiInput(InputEvent @event)
	{
		if(@event.IsActionPressed("left_click"))
		{
			EmitSignal(SignalName.Selected);
		}
	}
}
