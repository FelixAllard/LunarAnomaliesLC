using StaticNetcodeLib;
using Unity.Netcode;
using UnityEngine;

namespace LunarAnomalies;

[StaticNetcode]
public static class MoonsNetworkManageer
{
    [ClientRpc]
    public static void ExtendDeadlineClientRpc(int days)
    {
        float before = TimeOfDay.Instance.timeUntilDeadline;
        TimeOfDay.Instance.timeUntilDeadline += TimeOfDay.Instance.totalTime * days;
        TimeOfDay.Instance.UpdateProfitQuotaCurrentTime();
        TimeOfDay.Instance.SyncTimeClientRpc(TimeOfDay.Instance.globalTime, (int)TimeOfDay.Instance.timeUntilDeadline);
    }
    [ClientRpc]
    public static void ChangeScrapValueClientRpc()
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

    [ClientRpc]
    public static void EndOfRoundClientRpc()
    {
        Plugin.Logger.LogInfo("Destroying the moon on all clients!");
        if (LunarAnomaliesManager.moonGameObject != null)
        {
            Plugin.Logger.LogInfo("Current mon found, Attempting destruction");
            GameObject.Destroy(LunarAnomaliesManager.moonGameObject);
            LunarAnomaliesManager.StopUpdatingMoon();
        }
        else
        {
            if (GameObject.FindObjectOfType<LightupScript>())
            {
                GameObject.Destroy(GameObject.FindObjectOfType<LightupScript>().gameObject);
            }
        }
    }
}