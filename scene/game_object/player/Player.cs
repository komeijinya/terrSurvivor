using Godot;
using Godot.Collections;
using System;
using System.Reflection.Metadata;

public partial class Player : CharacterBody2D
{
	[Signal] public delegate void PlayerDiedEventHandler();
	[Export] float MaxHealth = 100;
	private HealthComponent healthComponent;

	public MoveComponent MoveComponent;
	public AnimatedSprite2D AnimatedSprite2D;

	public override void _Ready()
	{
		MoveComponent = GetNode<MoveComponent>("MoveComponent");
		AnimatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		healthComponent = GetNode<HealthComponent>("HealthComponent");
		healthComponent.MaxHealth = MaxHealth;
		healthComponent.Died += OnDied;
		GameEvent.Instance.UpdateUpgrade += OnUpdateUprade;
		
	}

	
	public override void _Process(double delta)
	{
		
		Vector2 direction = GetMoveDirection();
		if(direction != Vector2.Zero)
		{
			AnimatedSprite2D.Play("walk");
		}
		else
		{
			 AnimatedSprite2D.Play("stand"); 
		}
		if (direction.X > 0) 
		{
			AnimatedSprite2D.FlipH = true;
		}
		else if(direction.X < 0) 
		{
			AnimatedSprite2D.FlipH = false;
		}
		MoveComponent.AccelerateInDirection(direction);
		MoveComponent.Move(this);

	}

	public Vector2 GetMoveDirection()
	{
		Vector2 direction = new Vector2();
		direction = Input.GetVector("move_left","move_right","move_up","move_down");
		return direction;
	}

	public void OnDied()
	{
		EmitSignal(SignalName.PlayerDied);
	}

	public void OnUpdateUprade(Dictionary currentUpgrades,ability_upgrade chosenAbility)
	{
		if(chosenAbility is Ability)
		{
			var ability = chosenAbility as Ability;
			Node2D abilities = GetNode<Node2D>("Abilities");
			abilities.AddChild(ability.AbilityManagerScene.Instantiate());
		}
	}


}
