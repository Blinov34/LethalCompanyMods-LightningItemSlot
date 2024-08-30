using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using LightningItemSlot.Patches;
using LightningItemSlot.Utilities;
using UnityEngine;

namespace LightningItemSlot
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource MLS { get; private set; }
        private const string UISection = "UI";
        private readonly Harmony harmony = new Harmony(Metadata.GUID);
        private static Plugin Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            MLS = Logger;

            // Load info about any external mods first
            AssetBundleHelper.Initialize();

            MLS.LogInfo("Configuration Initialized.");


            harmony.PatchAll(typeof(StormyWeatherPatch));
            harmony.PatchAll(typeof(HUDManagerPatch));
            MLS.LogInfo("StormyWeather patched.");

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}