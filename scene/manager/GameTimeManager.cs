using Godot;
using System;

public partial class GameTimeManager : Node
{
    public Timer Timer;
    [Export] PackedScene EndScreen;

    [Signal] public delegate void DifficultUpdateEventHandler(int CurrentDifficult);

    public int CurrentDifficult = 0;
    public override void _Ready()
    {
        Timer = GetNode<Timer>("Timer");
        Timer.Timeout += OnTimerOut;
    }

    public override void _Process(double delta)
    {
        double nextTimeTarget = Timer.WaitTime - (CurrentDifficult + 1) * 10;
        if(Timer.TimeLeft <= nextTimeTarget)
        {
            CurrentDifficult += 1;
            EmitSignal(SignalName.DifficultUpdate,CurrentDifficult);
        }
    }

    public void OnTimerOut()
    {
        EndScreen endScreenInstance = EndScreen.Instantiate<EndScreen>();
        AddChild(endScreenInstance);
    }

    public float GetTime()
    {
        float time = (float)(Timer.WaitTime - Timer.TimeLeft);
        return time;
    }
}
