using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public partial class WeightedTable : Node
{
    public List<Dictionary> Items = new List<Dictionary>();

    public float WeightSum = 0;
    public int Count = 0;

    public void AddItem(Resource item,float weight)
    {   
        Items.Add(new Dictionary{{"item",item},{"weight",weight}});
        WeightSum += weight;
        Count++;
    }

    public Resource PickItem(List<Dictionary> excloud = null)
    {
        List<Dictionary> AdjustedItems = Items;
        float AdjustedSum = WeightSum;
        if(excloud != null && excloud.Count > 0)
        {
            foreach(var item in Items)
            {  
                if(excloud.Contains(item))
                {
                    AdjustedItems.Remove(item);
                    AdjustedSum -= item["weight"].AsInt32();
                }
            } 
        }

        if(AdjustedItems.Count == 0 || AdjustedSum ==0)
        {
            return null;
        }
        Random random = new Random();
        double chosenWeight = AdjustedSum * random.NextDouble();
        int iterationSum = 0;
        foreach(Dictionary item in AdjustedItems)
        {
           iterationSum += item["weight"].AsInt32();
           if(chosenWeight <= iterationSum)
           {
                return (Resource)item["item"];
           }
        }

        return null;
    }

  
}
