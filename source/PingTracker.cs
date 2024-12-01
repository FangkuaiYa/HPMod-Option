using HarmonyLib;
using UnityEngine;

namespace Hanpi.CustomOption
{
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {
        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            var position = __instance.GetComponent<AspectPosition>();
            position.DistanceFromEdge = new Vector3(3.6f, 0.1f, 0);
            position.AdjustPosition();

            __instance.text.text +=
                $"\n<color=#20D2FF>Custom Option API</color>\n";
        }
    }
}