using Godot;
using System;
using System.Data.Common;

public partial class GrenadeSkill : BaseSkill
{

    ShootComponent shootComponent;

    new public string ID = "grenade_skill";
    [Export] float BaseRadius = 90f;
    

    public override void _Ready()
    {
        shootComponent = GetNode<ShootComponent>("ShootComponent");
        shootComponent.SetCurrent();
        shootComponent.SetExplosion(BaseRadius);
        base._Ready();

    }



    public override void Shoot()
    {
        if (available)
        {   
            shootComponent.Direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
            shootComponent.shoot();
            
            available = false;
            timer.Start();
        }

    }




}
