using Godot;
using System;
using System.Net.Http.Headers;

public partial class DropComponent : Node
{
    [Export] private HealthComponent healthComponent;
    [Export] private PackedScene exp;

    [Export] public float DropPercent = 0.5f;
    public override void _Ready()
    {
        healthComponent.Died += OnDied;
    }


    public void InstanceDropItem()
    {
        Vector2 SpawnPosition = (Owner as Node2D).GlobalPosition;
        var ExpInstance = exp.Instantiate<Experience>();
        ExpInstance.GlobalPosition = SpawnPosition;
        GetTree().GetFirstNodeInGroup("Drop").AddChild(ExpInstance);

    }

    public void OnDied()
    {
        Random random = new Random();
        if( random.NextDouble() > DropPercent)
        {
            return;
        }

        if(exp == null)
        {
            return;
        }

        if(Owner == null)
        {
            return;
        }


        CallDeferred("InstanceDropItem");
    }
} 
