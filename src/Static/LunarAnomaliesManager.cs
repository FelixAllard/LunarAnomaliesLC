using System;
using System.Collections.Generic;
using System.Linq;
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
    //TODO reput 770
        Plugin.Logger.LogInfo(RoundManager.Instance.timeScript.globalTime.ToString());
        if (RoundManager.Instance.timeScript.globalTime >= 150)
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
        moons.Reverse();
        foreach (var moon in moons)
        {
            if (moon.precentageChanceSpawn >= UnityEngine.Random.Range(0f, 100f))
            {
                SpawnMoonClientRpc(moon.name); ;
                //StartUpdatingMoon(moon.timeBetweenEachCall);
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
        
        MonoBehaviourHelper.Instance.InvokeRepeatCallMoon(interval);
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
    public static void CreateCanvasAndText(string textContent)
    {
        // Create a new Canvas GameObject
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Create a new Text GameObject
        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(canvasGO.transform);

        // Set up RectTransform for the Text
        RectTransform rectTransform = textGO.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(600, 200);
        rectTransform.anchoredPosition = Vector2.zero;

        // Add Text component
        TextMesh text = textGO.AddComponent<TextMesh>();
        text.text = textContent;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 50;
        text.color = Color.white;
    }

}