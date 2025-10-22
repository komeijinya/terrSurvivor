using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public partial class AbilityManager : Node
{


    [Export] public LevelManager levelManager;

    [Export] public PackedScene UpgradeScreen;
    public WeightedDictionary<string,ability_upgrade> UpgardesPool = new WeightedDictionary<string,ability_upgrade>();

    public Dictionary CurrentUpgrades = new Dictionary { };
    
    

    private static ability_upgrade KnifeUpgrade = GD.Load<ability_upgrade>("res://upgrades/knife_upgrade.tres");

    private static ability_upgrade SwordUpgrade = GD.Load<ability_upgrade>("res://upgrades/sword_upgrade.tres");

    

    
    private static ability_upgrade Sword = GD.Load<Ability>("res://upgrades/sword.tres");
    public override void _Ready()
    {
        UpgardesPool.AddOrUpdate(KnifeUpgrade.Id,KnifeUpgrade,10);
        UpgardesPool.AddOrUpdate(Sword.Id, Sword, 10);
        //UpgardesPool.AddOrUpdate(SwordProjectile.Id,SwordProjectile,10);
        levelManager.LevelUp += OnLevelUp;

        GameEvent.Instance.UpdateUpgradesPool += OnUpdateUpgradesPool;
        

        

    }

    public List<ability_upgrade> PickUpgrades()
    {
        List<ability_upgrade> chosenUpgrades = new List<ability_upgrade>();
        WeightedDictionary<string,ability_upgrade> currentWeightPool = UpgardesPool.Clone();
        for(int i = 0; i<UpgardesPool.Count ; i++)
        {
            if(chosenUpgrades.Count == UpgardesPool.Count)
            {
                break;
            }
            ability_upgrade chosenUpgrade = currentWeightPool.GetRandom();
            chosenUpgrades.Add(chosenUpgrade);           
            currentWeightPool.Remove(chosenUpgrade.Id);
        }
        
        return chosenUpgrades;
    }

    public void ApplyUpgrade(ability_upgrade upgrade)
    {
        if(upgrade == null)
        {
            return;
        }


        if(upgrade is Ability)
        {
            LocalUpdateUpgradesPool(upgrade as Ability);
        }


        if(!CurrentUpgrades.ContainsKey(upgrade.Id))
        {
            CurrentUpgrades[upgrade.Id] =new Dictionary{{"resource",upgrade},{"quantity",1}};
        }
        else
        {
            int currentQuantity = ((Dictionary)CurrentUpgrades[upgrade.Id])["quantity"].As<int>();
            currentQuantity += 1;
            ((Dictionary)CurrentUpgrades[upgrade.Id])["quantity"] = currentQuantity;
        }


        if((int)((Dictionary)CurrentUpgrades[upgrade.Id])["quantity"]  == upgrade.MaxQuantity)
        {
            UpgardesPool.Remove(upgrade.Id);
        }


        if(upgrade.IsExclusion)
        {
            UpgardesPool.Remove(upgrade.ExclusionID);
        }
    }



    public void OnLevelUp(int currentLevel)
    {
        UpgradeScreen upgradeScreenInstance = UpgradeScreen.Instantiate<UpgradeScreen>();
        AddChild(upgradeScreenInstance);
        List<ability_upgrade> chosenUpgrades = PickUpgrades();
        upgradeScreenInstance.SetAbilityUpgrade(chosenUpgrades);
        upgradeScreenInstance.UpgradeSelected += OnUpgradeSelected;
        
    }

    public void OnUpgradeSelected(ability_upgrade upgrade)
    {
        ApplyUpgrade(upgrade);
        GameEvent.Instance.EmitUpdateUpgrade(CurrentUpgrades,upgrade);
        
    }

    public void LocalUpdateUpgradesPool(Ability ability)
    {
        if(ability.Id == "sword")
        {
            UpgardesPool.AddOrUpdate(SwordUpgrade.Id,SwordUpgrade,10);
        }
    }

    public void OnUpdateUpgradesPool(ability_upgrade ability_Upgrade,float weight)
    {
        
        UpgardesPool.AddOrUpdate(ability_Upgrade.Id,ability_Upgrade,weight);
    }


}
