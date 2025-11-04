using Godot;
using System;

public partial class BoomEffect : GpuParticles2D
{
    public override void _Ready()
    {
        Emitting = true;
        Finished += OnFineshed;
    }

    public override void _Process(double delta)
    {
        
        base._Process(delta);

    }

    public void OnFineshed()
    {
        QueueFree();
    }
}
