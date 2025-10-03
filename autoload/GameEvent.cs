using Godot;
using System;
using Godot.Collections;

public partial class GameEvent : Node
{

    [Signal] public delegate void ExpBallCollectedEventHandler(float num);

    [Signal] public delegate void UpdateUpgradeEventHandler(Dictionary currentUpgrades,ability_upgrade chosenAbility);

    [Signal] public delegate void UpdateUpgradesPoolEventHandler(ability_upgrade ability,float weight);

    public static GameEvent Instance{get;private set;}

    public override void _Ready()
    {
        Instance = this;
    }

    public void EmitExpBallCollected(float num)
    {
        EmitSignal(SignalName.ExpBallCollected,num);

    }

    public void EmitUpdateUpgrade(Dictionary currentUpgrades,ability_upgrade chosenUpgrade)
    {
        EmitSignal(SignalName.UpdateUpgrade,currentUpgrades,chosenUpgrade);
    }

    public void EmitUpdateUpgradesPool(ability_upgrade ability,float weight)
    {
        EmitSignal(SignalName.UpdateUpgradesPool,ability,weight);
    }




}
