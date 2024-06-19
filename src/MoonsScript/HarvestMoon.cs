using System;
using UnityEngine;

namespace LunarAnomalies.MoonsScript;

public class HarvestMoon : Moon
{
    public string name { get; set; } = "Harvest Moon";
    public Color color { get; set; } = new Color(218,206,10,255);
    public GameObject moonObject { get; set; }
    public float precentageChanceSpawn { get; set; } = 10f;
    public int timeBetweenEachCall { get; set; } = 4;
    public string messageMoon { get; set; } = "Loot value doubled";

    public void Init(GameObject gameObject)
    {
        moonObject = gameObject;
    }

    public void ApplyImmediateEffect()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("PhysicsProp"))
        {
            PhysicsProp itemPhysicProp = item.GetComponent<PhysicsProp>();
            if (itemPhysicProp != null)
            {
                if (itemPhysicProp.isInFactory)
                {
                    itemPhysicProp.SetScrapValue(itemPhysicProp.scrapValue*2);
                }
            }
        }
    }

    
    public void ApplyConstantEffect()
    {
        //No Constant Effect
    }
}