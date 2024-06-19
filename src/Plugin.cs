using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalLib.Modules;
using LunarAnomalies.MoonsScript;
using LunarAnomalies.Patch;
using UnityEngine;

namespace LunarAnomalies {
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(StaticNetcodeLib.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin {
        internal static new ManualLogSource Logger = null!;
        public static AssetBundle? ModAssets;
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        private void Awake() {
            Logger = base.Logger;
            InitializeNetworkBehaviours();
            string bundleName = "lunaranomaliesmodassets";
            string bundlePath =Path.Combine(Path.GetDirectoryName(Info.Location), bundleName);
            Logger.LogInfo($"Attempting to load AssetBundle from path: {bundlePath}");

            if (!File.Exists(bundlePath))
            {
                Logger.LogError($"AssetBundle file does not exist at path: {bundlePath}");
                return;
            }

            ModAssets = AssetBundle.LoadFromFile(bundlePath);

            if (ModAssets == null)
            {
                Logger.LogError("Failed to load custom assets.");
                return;
            }

            Logger.LogInfo("AssetBundle loaded successfully.");
            // Now you can load your assets from the bundle
            var goldMoon = ModAssets.LoadAsset<GameObject>("GoldMoon");
            var blueMoon = ModAssets.LoadAsset<GameObject>("BlueMoon");
            var redMoon = ModAssets.LoadAsset<GameObject>("RedMoon");
            LunarAnomaliesManager.SetMoon<HarvestMoon>(moon => moon.Init(goldMoon));
            LunarAnomaliesManager.SetMoon<DiamondMoon>(moon => moon.Init(blueMoon));
            LunarAnomaliesManager.SetMoon<BloodMoon>(moon => moon.Init(redMoon));
            harmony.PatchAll(typeof(StartOfRoundPatch));
            harmony.PatchAll(typeof(EntranceTeleport));
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private static void InitializeNetworkBehaviours()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static
                );
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(
                        typeof(RuntimeInitializeOnLoadMethodAttribute),
                        false
                    );
                    if (attributes.Length > 0)
                    {
                        method.Invoke(null, null);
                    }
                }

                // Check if the type is assignable to EnemyAI
                if (typeof(EnemyAI).IsAssignableFrom(type))
                {
                    // Patch the type here as needed
                    // For example, you could check for specific methods or attributes
                    // and invoke them similarly to RuntimeInitializeOnLoadMethodAttribute
                }
            }
        }



    }
}
