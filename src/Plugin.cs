using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using LethalLib.Modules;
using LunarAnomalies.MoonsScript;
using UnityEngine;

namespace LunarAnomalies {
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(StaticNetcodeLib.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin {
        internal static new ManualLogSource Logger = null!;
        public static AssetBundle? ModAssets;

        private void Awake() {
            Logger = base.Logger;
            InitializeNetworkBehaviours();
            var bundleName = "lunaranomaliesmodassets";
            ModAssets = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Info.Location), bundleName));
            if (ModAssets == null) {
                Logger.LogError($"Failed to load custom assets.");
                return;
            }
            var moonAsset = ModAssets.LoadAsset<GameObject>("moonAsset");
            LunarAnomaliesManager.Init(moonAsset);
            LunarAnomaliesManager.SetMoon<BloodMoon>(moon => moon.Init(LunarAnomaliesManager.Moon));

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
