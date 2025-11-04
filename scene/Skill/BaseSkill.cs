using Godot;
using System;

public partial class BaseSkill : Node2D
{
    [Export] public float BaseCD = 1;

    [Export] public string ID = "base_skill";
    [Export] public bool available = true;

    [Export] public Timer timer;

    public override void _Ready()
    {

        timer = GetNode<Timer>("Timer");
        timer.WaitTime = BaseCD;
        timer.Timeout += OnTimeOut;

    }

    public virtual void Shoot()
    {
        
    }

    public void OnTimeOut()
    {
        available = true;
    }

}
