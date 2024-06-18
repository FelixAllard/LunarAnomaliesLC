using Unity.Netcode;
using UnityEngine;

namespace LunarAnomalies.MoonsScript;

public class DiamondMoon : Moon
{
    public string name { get; set; }
    public Color color { get; set; }
    public GameObject moonObject { get; set; }
    public float precentageChanceSpawn { get; set; }
    public int timeBetweenEachCall { get; set; }
    public void Init(GameObject gameObject)
    {
        throw new System.NotImplementedException();
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