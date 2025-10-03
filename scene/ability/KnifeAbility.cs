using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class KnifeAbility : BasicWeapon
{
    [Export] PackedScene Knife;


    int BasePenetrate = 1;

    int currentPenetrate;

    public Vector2 Direction = Vector2.Right;

    float Offset = 20;
    private static ability_upgrade KnifeAim = GD.Load<ability_upgrade>("res://upgrades/knife_aim.tres");

    private static ability_upgrade KnifeDouble = GD.Load<ability_upgrade>("res://upgrades/knife_double.tres");
    Random random = new Random();
    private Timer timer;

    bool isaim = false;

    int shoottime = 1;

    public override void _Ready()
    {
        BaseCD = 1.0;
        BaseDamage = 7;
        timer = GetNode<Timer>("Timer");
        timer.WaitTime = BaseCD;
        timer.Timeout += OnTimeOut;
        timer.Start();
        GameEvent.Instance.UpdateUpgrade += OnUpdateUprade;

        GD.Print(KnifeAim);

    }

    public override void _Process(double delta)
    {
        if (!isaim)
        {
            GetShootDirection();
        }

    }

    public void GetShootDirection()
    {
        var CurrentDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        if (CurrentDirection != Vector2.Zero)
        {
            Direction = CurrentDirection;
        }
    }

    public void OnTimeOut()
    {

        for (int i = 0; i < shoottime; i++)
        {
            var KnifeInstance = Knife.Instantiate<Knife>();
            KnifeInstance.GlobalPosition = (GetTree().GetFirstNodeInGroup("Player") as Player).GlobalPosition + Vector2.One * ((float)random.NextDouble() * 2 - 1) * Offset;
            if (!isaim)
            {
                KnifeInstance.Direction = Direction;
            }
            else if (isaim)
            {
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
                Direction = (enemies[0].GlobalPosition - playerPosition).Normalized();
                KnifeInstance.Direction = Direction;
            }

            KnifeInstance.LookAt(KnifeInstance.GlobalPosition + Direction);
            KnifeInstance.Damage = BaseDamage;
            KnifeInstance.Penetrate = currentPenetrate;
            GetTree().GetFirstNodeInGroup("Projectile").AddChild(KnifeInstance);
        }

    }


    public Vector2 GetOffSet()
    {

        Vector2 offesetdirection = new Vector2(((float)random.NextDouble() * 2 - 1), ((float)random.NextDouble() * 2 - 1)) * Offset;
        return offesetdirection;
    }

    public void OnUpdateUprade(Dictionary currentUpgrades, ability_upgrade chosenAbility)
    {
        if (chosenAbility.Id == "knife_upgrade")
        {
            double currentCD = BaseCD * (1 - ((float)((Dictionary)currentUpgrades["knife_upgrade"])["quantity"] * .15f));
            currentPenetrate = BasePenetrate + (int)((Dictionary)currentUpgrades["knife_upgrade"])["quantity"];
            timer.WaitTime = currentCD;
            timer.Start();

            if ((int)((Dictionary)currentUpgrades["knife_upgrade"])["quantity"] == 4)
            {
                GameEvent.Instance.EmitUpdateUpgradesPool(KnifeAim, 10);
                GameEvent.Instance.EmitUpdateUpgradesPool(KnifeDouble, 10);
            }

        }

        if (chosenAbility.Id == "knife_aim")
        {
            isaim = true;
        }

        if (chosenAbility.Id == "knife_double")
        {
            shoottime += 1;
        }


    }

}
