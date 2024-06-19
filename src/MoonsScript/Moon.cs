using System;
using UnityEngine;

namespace LunarAnomalies.MoonsScript;

public interface Moon
{
    public String name
    {
        get;
        set;
    }
    public Color color
    {
        get;
        set;
    }
    public GameObject moonObject
    {
        get;
        set;
    }

    public float precentageChanceSpawn
    {
        get;
        set;

    }

    public int timeBetweenEachCall
    {
        get;
        set;
    }

    public string headerText
    {
        get;
        set;
    }
    public string messageMoon
    {
        get;
        set;
    }
    public void Init(GameObject gameObject);
    public void ApplyImmediateEffect();
    public void ApplyConstantEffect();


}