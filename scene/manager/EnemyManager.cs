using Godot;
using System;

public partial class EnemyManager : Node
{
    [Export] public PackedScene Zombie;

    [Export] public PackedScene Slime;
    [Export] public GameTimeManager GameTimeManager;

    WeightedDictionary<string,PackedScene> enemyTable = new WeightedDictionary<string,PackedScene>();

    public int SPAWNRADIUS = 500;

    public Timer Timer;

    public double BaseSpawnTime = 3;

    public override void _Ready()
    {
        enemyTable.AddOrUpdate("Slime",Slime,20);
        Timer = GetNode<Timer>("Timer");
        Timer.Timeout += OnTimeOut;
        GameTimeManager.DifficultUpdate += OnDifficultUpdate;
    }

    public Vector2 GetSpawnPosition()
    {
        Player player = GetTree().GetFirstNodeInGroup("Player") as Player;
        if(player is null)
        {
            return Vector2.Zero;
        }
        Vector2 SpawnPosition = Vector2.Zero;
        Random random = new Random();
        Vector2 RandomDirection = Vector2.Right.Rotated((float)(System.Math.Tau * random.NextDouble()));
        SpawnPosition = player.GlobalPosition + RandomDirection * SPAWNRADIUS;

        return SpawnPosition;
    }

    public void OnTimeOut()
    {
        Timer.Start();

        var enemyInstance = enemyTable.GetRandom().Instantiate<Node2D>();
        enemyInstance.GlobalPosition = GetSpawnPosition();
         GetTree().GetFirstNodeInGroup("Enemies").AddChild(enemyInstance);
        

    }

    public void OnDifficultUpdate(int currentDifficult)
    {
        Timer.WaitTime = BaseSpawnTime - (float)currentDifficult * 0.1;
        if(currentDifficult >= 4)
        {
            enemyTable.AddOrUpdate("Zombie",Zombie,10);
        }
    }
}
