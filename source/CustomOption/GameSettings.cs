using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CustomOption.CustomOption;
using HarmonyLib;
using Reactor.Utilities.Extensions;

namespace CustomOption
{
    [HarmonyPatch]
    public static class GameSettings
    {
        public static bool AllOptions;

        [HarmonyPatch]
        private static class GameOptionsDataPatch
        {
            public static IEnumerable<MethodBase> TargetMethods()
            {
                return typeof(GameOptionsData).GetMethods(typeof(string), typeof(int));
            }

            private static void Postfix(ref string __result)
            {
                var builder = new StringBuilder(AllOptions ? __result : "");

                foreach (var option in CustomOption.CustomOption.AllOptions)
                {
                    builder.AppendLine($"{option.Name}");
                }

                __result += "\n\n" + builder.ToString();
                __result = $"<size=1.25>{__result}</size>";
            }
        }

        [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Update))]
        public static class Update
        {
            public static void Postfix(ref GameOptionsMenu __instance)
            {
                __instance.GetComponentInParent<Scroller>().ContentYBounds.max = (__instance.Children.Length - 6.5f) / 2;
            }
        }
    }
}