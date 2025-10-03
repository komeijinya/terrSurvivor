using Godot;
using System;

[GlobalClass]
public partial class ability_upgrade : Resource
{
    [Export] public string Id;
    [Export] public string Name;

    [Export] public string Description;

    [Export] public float weight = 10;

    [Export] public int MaxQuantity = 0;

    [Export] public bool IsExclusion = false;

    [Export] public string ExclusionID;

    
}
