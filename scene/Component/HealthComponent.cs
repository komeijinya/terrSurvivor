using Godot;
using System;

public partial class HealthComponent : Node
{
    [Signal] public delegate void DiedEventHandler();
    [Signal] public delegate void HurtEventHandler(float healthPercent);
    [Export] public float MaxHealth = 100f;
    public float CurrentHealth;

    public override async void _Ready()
    {
        await ToSignal(GetOwner(),Node.SignalName.Ready);
        CurrentHealth = MaxHealth;
    }

    public void Damage(float value)
    {
        CurrentHealth = (CurrentHealth - value > 0 ? CurrentHealth - value : 0);
        EmitSignal(SignalName.Hurt,CurrentHealth/MaxHealth);
        CheckDied();
    }

    public void CheckDied()
    {
        if(CurrentHealth == 0)
        {
            EmitSignal(SignalName.Died);
        }
    }
    
}
