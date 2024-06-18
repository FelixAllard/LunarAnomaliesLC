using System;
using System.Collections.Generic;
using LunarAnomalies.MoonsScript;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using LightType = UnityEngine.LightType;

namespace LunarAnomalies;

public static class LunarAnomaliesManager
{
    public static GameObject YellowMoon;
    public static GameObject BlueMoon;
    public static GameObject RedMoon;
    public static GameObject Moon;
    public static List<Moon> moons;

    public static void Init(GameObject moon)
    {
        Moon = moon;
        moons = new List<Moon>();
    }
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

    public static void StartUpdatingMoon(float interval)
    {
        // Call UpdateMoon function every 'interval' seconds, starting after 0 seconds
        // You can adjust the initial delay if needed
        // Unity will automatically handle the repeating calls
        // The method will be called on the first frame and then repeatedly every 'interval' seconds
        MonoBehaviourHelper.Instance.InvokeRepeating(nameof(UpdateMoon), 0f, interval);
    }

    // Call this method to stop the repeating function
    public static void StopUpdatingMoon()
    {
        // Cancel the repeating invocation of UpdateMoon
        MonoBehaviourHelper.Instance.CancelInvoke(nameof(UpdateMoon));
    }

    public static void UpdateMoon()
    {
        
    }

}