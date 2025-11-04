using Godot;
using System;

public partial class TestEnemy : Zombie
{
    public override void OnHurt()
    {
        base.OnHurt();
        GD.Print("Oh!");
    }

    public override void _Draw()
    {
        
    }


}
