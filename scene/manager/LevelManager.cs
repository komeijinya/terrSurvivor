using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class LevelManager : Node
{

    [Signal] public delegate void ExpChangedEventHandler(float CurrentExprience,float TargetExprience);
    [Signal] public delegate void LevelUpEventHandler(int CurrentLevel);

    public int CurrentLevel = 1;
    public float CurrentExprience = 0;

    public float TargetExprience = 1;

    public const float TargetExprienceGrow = 5;
    public override void _Ready()
    {
        GameEvent.Instance.ExpBallCollected +=OnExpBallCollected;
    }

    public void OnExpBallCollected(float num)
    {
        if(CurrentExprience + num >= TargetExprience)
        {
            float TotalExp = CurrentExprience + num;
            while(TotalExp >= TargetExprience)
            {
                CurrentLevel +=1;
                EmitSignal(SignalName.LevelUp,CurrentLevel);
                TotalExp -= TargetExprience;
                TargetExprience += TargetExprienceGrow;
                EmitSignal(SignalName.LevelUp,TargetExprience,TargetExprience);
            }
            CurrentExprience = TotalExp;
        }
        else
        {
            CurrentExprience += num;
            EmitSignal(SignalName.LevelUp,CurrentExprience,TargetExprience);
        }

        //GD.Print("exp: ",CurrentExprience,"\ntar: ",TargetExprience,"\nlevel: ",CurrentLevel);
    }
}
