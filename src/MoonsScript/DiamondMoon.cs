using Unity.Netcode;
using UnityEngine;

namespace LunarAnomalies.MoonsScript;

public class DiamondMoon : Moon
{
    public string name { get; set; } = "Diamond Moon";
    public Color color { get; set; } = new Color(20, 141, 224, 255);
    public GameObject moonObject { get; set; }
    public float precentageChanceSpawn { get; set; } = 10f;
    public int timeBetweenEachCall { get; set; } = 60;
    public void Init(GameObject gameObject)
    {
        moonObject = gameObject;;
    }

    public void ApplyImmediateEffect()
    {
        MoonsNetworkManageer.ExtendDeadlineClientRpc(1);
    }
    

    public void ApplyConstantEffect()
    {
        throw new System.NotImplementedException();
    }
}