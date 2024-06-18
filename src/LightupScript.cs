using System;
using System.Collections;
using UnityEngine;

namespace LunarAnomalies;

public class LightupScript : MonoBehaviour
{
    public float duration = 30f;
    private float targetValue = 0.1612916f;
    private float currentValue = 0f;
    public Light light;
    void Start()
    {
        StartCoroutine(MoveValueOverTime());
    }

    IEnumerator MoveValueOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            light.intensity = Mathf.Lerp(0, targetValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        light.intensity = targetValue; // Ensure the final value is set exactly to the target
    }
}