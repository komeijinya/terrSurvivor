using Godot;
using System;

public partial class Main : Node
{
    Player Player;

    [Export] PackedScene EndScene;

    public override void _Ready()
    {
        Player = GetNode<Player>("%Player");
        Player.PlayerDied += OnPlayerDied;
    }

    public void OnPlayerDied()
    {
        EndScreen EndSceneInstance = EndScene.Instantiate<EndScreen>();
        AddChild(EndSceneInstance);
        EndSceneInstance.SetDefeat();
    }
}
