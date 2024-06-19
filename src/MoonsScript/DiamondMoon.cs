using Unity.Netcode;
using UnityEngine;

namespace LunarAnomalies.MoonsScript;

public class DiamondMoon : Moon
{
    public string name { get; set; } = "Diamond Moon";
    public Color color { get; set; } = new Color(20, 141, 224, 255);
    public GameObject moonObject { get; set; }
    public float precentageChanceSpawn { get; set; } = 100f;
    public int timeBetweenEachCall { get; set; } = 60;
    public string headerText { get; set; } = "A Diamond Moon illuminates the night...";
    public string messageMoon { get; set; } = "Treasures and fortunes await those brave enough to seek them.";

    public void Init(GameObject gameObject)
    {
        moonObject = gameObject;;
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
                    itemPhysicProp.SetScrapValue(itemPhysicProp.scrapValue*(int)UnityEngine.Random.Range(1.5f,2.5f));
                }
            }
        }
    }
    

    public void ApplyConstantEffect()
    {
    }
}