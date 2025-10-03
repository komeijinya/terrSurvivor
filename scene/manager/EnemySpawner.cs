using Godot;
using System;

public partial class EnemySpawner : Node
{
    public Timer Timer;

    public double BaseSpawnTime = 3;
    public int SPAWNRADIUS = 500;

    WeightedDictionary<string, PackedScene> enemyTable = new WeightedDictionary<string, PackedScene>();


    EnemySpawner(double baseSpawnTime, WeightedDictionary<string, PackedScene> enemytable)
    {
        BaseSpawnTime = baseSpawnTime;
        enemyTable = enemytable;
    }
    public override void _Ready()
    {
        Timer.Timeout += OnTimeOut;
    }

    public void OnTimeOut()
    {
        Timer.Start();

        var enemyInstance = enemyTable.GetRandom().Instantiate<Node2D>();
        enemyInstance.GlobalPosition = GetSpawnPosition();
        GetTree().GetFirstNodeInGroup("Enemies").AddChild(enemyInstance);


    }

    public Vector2 GetSpawnPosition()
    {
        Player player = GetTree().GetFirstNodeInGroup("Player") as Player;
        if (player is null)
        {
            return Vector2.Zero;
        }
        Vector2 SpawnPosition = Vector2.Zero;
        Random random = new Random();
        Vector2 RandomDirection = Vector2.Right.Rotated((float)(System.Math.Tau * random.NextDouble()));
        SpawnPosition = player.GlobalPosition + RandomDirection * SPAWNRADIUS;

        return SpawnPosition;
    }
}
