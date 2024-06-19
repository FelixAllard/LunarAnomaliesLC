using System;
using System.Collections.Generic;
using LunarAnomalies.MoonsScript;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using LightType = UnityEngine.LightType;
using Random = System.Random;

namespace LunarAnomalies;

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
        if (RoundManager.Instance.timeScript.globalTime >= 770)
        {
            if (hasAlreadyTried == false)
            {
                hasAlreadyTried = true;
                ReachedNightServerRpc();
            }
            
        }
        else
        {
            hasAlreadyTried = false;
        }
        
    }
    [ServerRpc]
    public static void ReachedNightServerRpc()
    {
        foreach (var moon in moons)
        {
            if (moon.precentageChanceSpawn >= UnityEngine.Random.Range(0f, 100f))
            {
                SpawnMoonClientRpc(moon.name);
                moon.ApplyImmediateEffect();
                StartUpdatingMoon(moon.timeBetweenEachCall);
                currentMoon = moon;
                return;
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
                moonGameObject = GameObject.Instantiate(moon.moonObject);
            }
            
        }
    }
    public static void StartUpdatingMoon(float interval)
    {
        // Call UpdateMoon function every 'interval' seconds, starting after 0 seconds
        // You can adjust the initial delay if needed
        // Unity will automatically handle the repeating calls
        // The method will be called on the first frame and then repeatedly every 'interval' seconds
        MonoBehaviourHelper.Instance.InvokeRepeating(nameof(UpdateMoon), 3f, interval);
    }

    // Call this method to stop the repeating function
    public static void StopUpdatingMoon()
    {
        // Cancel the repeating invocation of UpdateMoon
        MonoBehaviourHelper.Instance.CancelInvoke(nameof(UpdateMoon));
    }
    public static void UpdateMoon()
    {
        currentMoon.ApplyConstantEffect();
    }

}