using System;
using System.Collections.Generic;
using System.Linq;
using LunarAnomalies.MoonsScript;
using StaticNetcodeLib;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using LightType = UnityEngine.LightType;
using Random = System.Random;

namespace LunarAnomalies;

[StaticNetcode]
public static class LunarAnomaliesManager
{

    public static GameObject Moon;
    public static List<Moon> moons =new List<Moon>();
    public static Moon currentMoon;
    private static bool hasAlreadyTried = false;
    public static GameObject moonGameObject;
    
    public static void SetMoon<T>(Action<T> initializationAction) where T : Moon, new()
    {
        // Create a new instance of the specified moon type
        T newMoon = new T();

        // Call the initialization action if provided
        initializationAction?.Invoke(newMoon);

        // Add the new moon instance to the moons list
        moons.Add(newMoon);
        // Now you can use 'newMoon' as the newly created moon instance
    }

    public static void TryApplyingEffect()
    {
        Plugin.Logger.LogInfo(RoundManager.Instance.timeScript.globalTime.ToString());
        if (RoundManager.Instance.timeScript.globalTime >= 730)
        {
            if (hasAlreadyTried == false)
            {
                hasAlreadyTried = true;
                if (RoundManager.Instance.IsHost)
                {
                    ReachedNight();
                }
            }
            
        }
        else
        {
            hasAlreadyTried = false;
        }
        
    }
    public static void ReachedNight()
    {
        if (moonGameObject == null)
        {
            moons.Reverse();
            foreach (var moon in moons)
            {
                if (moon.precentageChanceSpawn >= UnityEngine.Random.Range(0f, 100f))
                {
                    SpawnMoonClientRpc(moon.name);
                    //StartUpdatingMoon(moon.timeBetweenEachCall);
                    currentMoon = moon;
                    return;
                }
            }
        }
    }
    [ClientRpc]
    public static void SpawnMoonClientRpc(String name)
    {
        foreach (var moon in moons)
        {
            if (moon.name == name)
            {
                currentMoon = moon;
                moonGameObject = GameObject.Instantiate(moon.moonObject);
            }
            
        }
    }
    [ClientRpc]
    public static void TellPeopleMoonIsStartingClientRpc()
    {
        HUDManager.Instance.DisplayTip(currentMoon.headerText ,currentMoon.messageMoon, true);
    }
    // Call this method to stop the repeating function
    public static void StopUpdatingMoon()
    {
        Plugin.Logger.LogInfo("Called Moon " + currentMoon.name + " intervale!");
        // Cancel the repeating invocation of UpdateMoon
        MonoBehaviourHelper.Instance.CancelInvoke(nameof(UpdateMoon));
    }
    public static void UpdateMoon()
    {
        currentMoon.ApplyConstantEffect();
    }

}