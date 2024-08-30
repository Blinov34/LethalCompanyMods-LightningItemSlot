﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace LightningItemSlot.Utilities
{
    internal class AssetBundleHelper
    {
        private static AssetBundle _bundle;

        public static GameObject MonitorsPrefab { get; private set; }
        public static Sprite Reticle { get; private set; }
        public static GameObject MedStationPrefab { get; private set; }
        public static GameObject LightningOverlay { get; private set; }

        public static void Initialize()
        {
            if (_bundle == null)
            {
                // Find our directory
                string assetPath = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "LightningItemSlotAssets");
                if (!new FileInfo(assetPath).Exists)
                {
                    Plugin.MLS.LogError(Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName));
                    Plugin.MLS.LogError("Could not find asset bundle!");
                    return;
                }
                _bundle = AssetBundle.LoadFromFile(assetPath, 0x90880297);

                // Load assets into memory
                Plugin.MLS.LogInfo("Loading assets...");
                MonitorsPrefab = _bundle.LoadAsset<GameObject>("MonitorGroup.prefab");
                Reticle = _bundle.LoadAsset<Sprite>("reticle.png");
                MedStationPrefab = _bundle.LoadAsset<GameObject>("MedStation.prefab");
                LightningOverlay = _bundle.LoadAsset<GameObject>("LightningOverlay.prefab");
            }
        }
    }
}
