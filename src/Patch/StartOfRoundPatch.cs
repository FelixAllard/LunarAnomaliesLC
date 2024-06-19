using HarmonyLib;
using UnityEngine;

namespace LunarAnomalies.Patch;

[HarmonyPatch(typeof(RoundManager))]
internal class StartOfRoundPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void PostFixStartFunction(RoundManager __instance)
    {
        var helper = MonoBehaviourHelper.Instance;
    }
    [HarmonyPatch("UnloadSceneObjectsEarly")]
    [HarmonyPostfix]
    private static void PostFixStart(RoundManager __instance)
    {
        if (LunarAnomaliesManager.moonGameObject != null)
        {
            GameObject.Destroy(LunarAnomaliesManager.moonGameObject);
            LunarAnomaliesManager.StopUpdatingMoon();
        }
    }
}