using Godot;
using System;

public partial class BoomProjectile : BasicProjectile
{
    BoomArea boomArea;

    public override void _Ready()
    {
        base._Ready();
        boomArea = GetNode<BoomArea>("BoomArea");
        boomArea.Damage = Damage;
        boomArea.Radius = radius;
        boomArea.Set();
        GD.Print(boomArea.Radius);
    }

    public override void OnHit()
    {
        boomArea.Boom();
        base.OnHit();
    }

 



}
