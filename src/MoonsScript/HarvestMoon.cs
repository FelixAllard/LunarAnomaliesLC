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
    public string headerText { get; set; } = "The Harvest Moon casts its golden light...";
    public string messageMoon { get; set; } = "\nThe company is feeling generous";

    public void Init(GameObject gameObject)
    {
        moonObject = gameObject;
    }

    public void ApplyImmediateEffect()
    {
        MoonsNetworkManageer.ExtendDeadlineClientRpc(1);
    }

    
    public void ApplyConstantEffect()
    {
        //No Constant Effect
    }
}