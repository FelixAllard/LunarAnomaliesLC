# Moon Anomalies
Introduces moon rotation events for lethal company. This mod focuses on 4 main moon rotations for now, which is : {none ; blood moon ; diamond moon ; harvest moon}.

### Blood Moon  
Increases danger on moons by increasing the spawn rate of mobs by 2x.

### Harvest Moon 
Increases the value of scrap inside the facility by 1.5x-2.5x at random.

### Diamond Moon
Adds an extra day for the deadline before you have to sell.

Note that the most common one is a normal moon, but any of these other 3 might occur between the time of 7pm and midnight. The mod might get updates to add more moons variation and can work as an API to add new ones:

## Developers : 
Adding a new moon :
```c#
[BepInDependency(StaticNetcodeLib.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
```
Just above your plugin Awake statement.
Then : 
```c#
var goldMoon = ModAssets.LoadAsset<GameObject>("GoldMoon");
LunarAnomaliesManager.SetMoon<HarvestMoon>(moon => moon.Init(goldMoon));
```
The gold moon must be the prefab you want to be instantiated when the moon is yours. You must also create a moon script that inherits from Moon which will contain the moons property!
```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LunarAnomalies.MoonsScript;

public class BloodMoon : Moon
{
    public string name { get; set; } = "Blood Moon";
    public Color color { get; set; } = new Color(224,29,20,255);
    public GameObject moonObject { get; set; }
    //TODO put it back at 10f
    public float precentageChanceSpawn { get; set; } = 10f;
    public int timeBetweenEachCall { get; set; } = 4;
    public string messageMoon { get; set; } = "ESCAPE NOW!!!";

    public void Init(GameObject gameObject)
    {
        moonObject = gameObject;
    }

    public void ApplyImmediateEffect()
    {
        //Should be called by you in the script you will create in your moon prefab
    }

    public void ApplyConstantEffect()
    {
       //Must be invoked every "TimeBetweenEachCall" amount of time!
    }
}
```
You then call this when you want the moon effect to start. I usually do it in the script attached to the prefab
```c#
LunarAnomaliesManager.currentMoon.ApplyImmediateEffect();
LunarAnomaliesManager.TellPeopleMoonIsStartingClientRpc();
InvokeRepeating("MiddleManFunction", 0f, LunarAnomaliesManager.currentMoon.timeBetweenEachCall);
```
and you are done! The moon manager will use this to have the properties of the moon!
