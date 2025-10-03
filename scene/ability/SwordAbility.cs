using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class SwordAbility : BasicWeapon
{
    [Export] public PackedScene Sword;


    public Timer Timer;

    public override void _Ready()
    {
        BaseCD = 2f;
        BaseDamage = 20;
        ID = "Sword";
        BaseScale = Vector2.One * 1.2f;
        Timer = GetNode<Timer>("Timer");
        Timer.Timeout += OnTimeOut;
        Timer.WaitTime = BaseCD;
        Timer.Start();
        GameEvent.Instance.UpdateUpgrade += OnUpdateUprade;
    }



    public void OnTimeOut()
    {
        Timer.Start();
        Sword SwordInstance = Sword.Instantiate<Sword>();
        SwordInstance.GlobalPosition = (GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition;
        SwordInstance.BaseDamage = BaseDamage;
        SwordInstance.Scale = BaseScale;
        GetTree().GetFirstNodeInGroup("Projectile").AddChild(SwordInstance);
    }

    
    public void OnUpdateUprade(Dictionary currentUpgrades,ability_upgrade chosenAbility)
    {
        if(chosenAbility.Id == "sword_upgrade")
        {
            double currentCD = BaseCD * (1-((float)((Dictionary)currentUpgrades["sword_upgrade"])["quantity"] * .1f));
            BaseScale = Vector2.One * (1.2f +(float)((Dictionary)currentUpgrades["sword_upgrade"])["quantity"] * 0.1f);
            Timer.WaitTime = currentCD;
            Timer.Start();
        }
    }

}
