using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public partial class SkillComponent : Node2D
{
    [Export] public Array<PackedScene> SkillScene = new Array<PackedScene>();
    List<BaseSkill> Skills = new List<BaseSkill>();

    public override void _Ready()
    {
        GrenadeSkill grenadeSkill = new GrenadeSkill();
    }


    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("skill_1"))
        {
            if (Skills.Count == 0)
            {
                return;
            }
            else
            {
                Skills[0].Shoot();
            }
            
        }
    }

    public void AddSkill(Ability ability)
    {
        BaseSkill skill = ability.AbilityManagerScene.Instantiate<BaseSkill>();
        AddChild(skill);
        Skills.Add(skill);
    }

    public void RemoveSkill(BaseSkill skill)
    {
        Skills.Remove(skill);
    }

}
