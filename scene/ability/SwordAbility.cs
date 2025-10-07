using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SwordAbility : BasicWeapon
{
    [Export] public PackedScene Sword;


    public Timer Timer;

    ShootComponent shootComponent;

    public override void _Ready()
    {
        BaseCD = 2f;
        BaseDamage = 20;
        ID = "Sword";
        BaseScale = Vector2.One * 1.6f;
        Timer = GetNode<Timer>("Timer");
        Timer.Timeout += OnTimeOut;
        Timer.WaitTime = BaseCD;
        Timer.Start();
        GameEvent.Instance.UpdateUpgrade += OnUpdateUprade;
        shootComponent = GetNode<ShootComponent>("ShootComponent");
        shootComponent.SetCurrent();
    }



    public void OnTimeOut()
    {
        Timer.Start();
        Sword SwordInstance = Sword.Instantiate<Sword>();
        SwordInstance.GlobalPosition = (GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition;
        var enemies = GetTree().GetNodesInGroup("enemy").OfType<BasicEnemy>().ToList();
        if (enemies.Count == 0)
        {
            return;
        }
        Vector2 playerPosition = (GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition;
        enemies.Sort((a, b) =>
        {
            var distA = a.GlobalPosition.DistanceTo(playerPosition);
            var distB = b.GlobalPosition.DistanceTo(playerPosition);
            return distA.CompareTo(distB);
        });
        SwordInstance.Direction = (enemies[0].GlobalPosition - playerPosition).Normalized();
        SwordInstance.BaseDamage = BaseDamage;
        SwordInstance.LookAt(SwordInstance.GlobalPosition + SwordInstance.Direction);
        SwordInstance.Scale = BaseScale;
        GetTree().GetFirstNodeInGroup("Projectile").AddChild(SwordInstance);
        shootComponent.Position = Position + SwordInstance.Direction * 50f;;
        shootComponent.Direction = SwordInstance.Direction;
        shootComponent.shoot();
    }


    public void OnUpdateUprade(Dictionary currentUpgrades, ability_upgrade chosenAbility)
    {
        if (chosenAbility.Id == "sword_upgrade")
        {
            double currentCD = BaseCD * (1 - ((float)((Dictionary)currentUpgrades["sword_upgrade"])["quantity"] * .1f));
            BaseScale = Vector2.One * (1.6f + (float)((Dictionary)currentUpgrades["sword_upgrade"])["quantity"] * 0.2f);
            Timer.WaitTime = currentCD;
            Timer.Start();
        }
    }

}
