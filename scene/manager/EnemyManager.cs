using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyManager : Node
{
    [Export] public PackedScene Zombie;

    [Export] public PackedScene Slime;

    [Export] public PackedScene SkeletonArcher;
    [Export] public GameTimeManager GameTimeManager;
    [Export] public PackedScene EnemySpawner;

    

    WeightedDictionary<string,PackedScene> enemyTable = new WeightedDictionary<string,PackedScene>();

    List<EnemySpawner> Spawners = new List<EnemySpawner>();
    public int SPAWNRADIUS = 500;


    public override void _Ready()
    {
        
        enemyTable.AddOrUpdate("slime",Slime,20);
        enemyTable.AddOrUpdate(SkeletonArcher.Instantiate<BasicEnemy>().ID,SkeletonArcher,30);
        EnemySpawner spawnerInstance = EnemySpawner.Instantiate<EnemySpawner>();
        AddChild(spawnerInstance);
        spawnerInstance.SetEnemySpawner(enemyTable,3);
        Spawners.Add(spawnerInstance);
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


    public void OnDifficultUpdate(int currentDifficult)
    {

        Spawners[0].SetSpawnTime(Spawners[0].BaseSpawnTime - (float)currentDifficult * 0.1);
        if(currentDifficult >= 4)
        {
            Spawners[0].AddEnemy(Zombie,10);
        }
    }
}
