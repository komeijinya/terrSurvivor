using Godot;
using System;
using System.Collections.Generic;

public partial class UpgradeScreen : CanvasLayer
{
	public HBoxContainer CardContainer;
	[Signal] public delegate void UpgradeSelectedEventHandler(ability_upgrade upgrade);
	[Export] public PackedScene UpgradeCard;
	public override void _Ready()
	{
		CardContainer = GetNode<HBoxContainer>("MarginContainer/CardContainer");
		
		GetTree().Paused = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetAbilityUpgrade(List<ability_upgrade> upgrades)
	{
		if(upgrades.Count == 0)
		{
			GetTree().Paused = false;
			QueueFree();
		}

		foreach(ability_upgrade upgrade in upgrades)
		{
			AbilityUpgradeCard cardInstance = UpgradeCard.Instantiate<AbilityUpgradeCard>();
			CardContainer.AddChild(cardInstance);
			cardInstance.SetAbilityUpgrade(upgrade);
			cardInstance.Selected += () => OnUpgradeSelected(upgrade);
		}
	}

	public void OnUpgradeSelected(ability_upgrade upgrade)
	{
		EmitSignal(SignalName.UpgradeSelected,upgrade);
		GetTree().Paused = false;
		QueueFree();
	}
}
