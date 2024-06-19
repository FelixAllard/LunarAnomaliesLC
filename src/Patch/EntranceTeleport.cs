using HarmonyLib;
using UnityEngine;

namespace LunarAnomalies.Patch;
[HarmonyPatch(typeof(EntranceTeleport))]
public class EntranceTeleport
{
    [HarmonyPatch("TeleportPlayer")]
    [HarmonyPostfix]
    private static void PostFixStartFunction(EntranceTeleport __instance)
    {
        LightupScript lightManager = GameObject.FindObjectOfType<LightupScript>();
        if (GameNetworkManager.Instance.localPlayerController.isInsideFactory)
        {
            lightManager.light.enabled = false;
        }
        else
        {
            lightManager.light.enabled = true;
        }
    }
}