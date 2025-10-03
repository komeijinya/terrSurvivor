using Godot;
using System;

public partial class EnemySpawner : Node
{
    public Timer Timer;

    public double BaseSpawnTime = 3;
    public int SPAWNRADIUS = 500;

    WeightedDictionary<string, PackedScene> enemyTable = new WeightedDictionary<string, PackedScene>();


    public void SetEnemySpawner(WeightedDictionary<string, PackedScene> enemytable,double baseSpawnTime)
    {
        BaseSpawnTime = baseSpawnTime;
        enemyTable = enemytable.Clone();
    }

    
    public override void _Ready()
    {
        Timer = new Timer();
        Timer.WaitTime = BaseSpawnTime;
        AddChild(Timer);
        Timer.Timeout += OnTimeOut;
        Timer.Start();
    }

    public void OnTimeOut()
    {
        Timer.Start();

        var enemyInstance = enemyTable.GetRandom().Instantiate<BasicEnemy>();
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
    
    public void AddEnemy(PackedScene enemy,float weight)
    {
        enemyTable.AddOrUpdate(enemy.Instantiate<BasicEnemy>().ID,enemy,weight);
    }

    public void RemoveEnemy(PackedScene enemy)
    {
        enemyTable.Remove(enemy.Instantiate<BasicEnemy>().ID);
    }

    public void SetSpawnTime(double time)
    {
        if(time <= 0.1)
        {
            return;
        }
        Timer.WaitTime = time;
        Timer.Start();
    }


}
