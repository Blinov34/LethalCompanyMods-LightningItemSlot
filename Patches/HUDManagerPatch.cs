using GameNetcodeStuff;
using LightningItemSlot.Utilities;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

namespace LightningItemSlot.Patches
{
    internal static class HUDManagerPatch
    {
        public static GrabbableObject CurrentLightningTarget;
        private static List<SpriteRenderer> _lightningOverlays;

        [HarmonyPatch(typeof(HUDManager), nameof(Start))]
        [HarmonyPostfix]
        [HarmonyPriority(Priority.Low)]
        private static void Start(HUDManager __instance)
        {
            // Create lightning overlays on each inventory slot

            _lightningOverlays = new List<SpriteRenderer>();
            for (int i = 0; i < __instance.itemSlotIconFrames.Length; i++)
            {
                var overlay = Object.Instantiate(AssetBundleHelper.LightningOverlay, __instance.itemSlotIconFrames[i].transform);
                overlay.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                overlay.transform.localScale = Vector3.one;

                var sprite = overlay.GetComponent<SpriteRenderer>();
                sprite.enabled = false;
                _lightningOverlays.Add(sprite);
            }

        }

        [HarmonyPatch(typeof(HUDManager), nameof(Update))]
        [HarmonyPostfix]
        private static void Update(HUDManager __instance)
        {
            // Toggle lightning overlays on item slots when needed
            if (_lightningOverlays.Count > 0 && HUDManager.Instance != null && StartOfRound.Instance.localPlayerController != null)
            {
                for (int i = 0; i < Mathf.Min(HUDManager.Instance.itemSlotIconFrames.Length, _lightningOverlays.Count, StartOfRound.Instance.localPlayerController.ItemSlots.Length); i++)
                {
                    bool shouldBeEnabled = CurrentLightningTarget != null && StartOfRound.Instance.localPlayerController.ItemSlots[i] == CurrentLightningTarget;
                    if (_lightningOverlays[i].enabled != shouldBeEnabled)
                    {
                        _lightningOverlays[i].enabled = shouldBeEnabled;
                    }
                }
            }

        }

    }
}
