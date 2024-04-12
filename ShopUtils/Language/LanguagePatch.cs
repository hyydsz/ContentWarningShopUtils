using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace ShopUtils.Language
{
    [HarmonyPatch(typeof(Item))]
    internal static class LanguagePatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Item.GetTootipData))]
        private static bool GetTootipData(Item __instance, ref IEnumerable<IHaveUIData> __result)
        {
            if (Items.registerItems.Contains(__instance))
            {
                if (__instance.Tooltips.Count > 0)
                {
                    string text = __instance.name.Trim().Replace(" ", "") + "_ToolTips";
                    if (Languages.TryGetLanguage(text, out string language))
                    {
                        string[] array = language.Split(';');

                        __instance.Tooltips = new List<ItemKeyTooltip>();
                        foreach (string s in array)
                        {
                            __instance.Tooltips.Add(new ItemKeyTooltip(s));
                        }
                    }

                    __result = __instance.Tooltips;
                    return false;
                }
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Item.GetLocalizedDisplayName))]
        private static bool GetLocalizedDisplayName(Item __instance, ref string __result)
        {
            if (Items.registerItems.Contains(__instance))
            {
                string text = __instance.name.Trim().Replace(" ", "");
                if (Languages.TryGetLanguage(text, out string language))
                {
                    __result = language;
                    return false;
                }

                __result = __instance.displayName;
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(ShopItem))]
    internal static class ShopItemPatches
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, new Type[] { typeof(Item) })]
        private static IEnumerable<CodeInstruction> ShopItem(IEnumerable<CodeInstruction> instructions)
        {
            FieldInfo display = AccessTools.Field(typeof(Item), nameof(Item.displayName));
            MethodInfo method = AccessTools.Method(typeof(Item), nameof(Item.GetLocalizedDisplayName));

            MethodInfo LogError = AccessTools.Method(typeof(Debug), nameof(Debug.LogError), new Type[] { typeof(object) });

            return new CodeMatcher(instructions)

                .SearchForward(code => code.opcode == OpCodes.Ldfld && (FieldInfo) code.operand == display)
                .ThrowIfInvalid("Couldn't find displayName")
                .SetInstructionAndAdvance(new CodeInstruction(OpCodes.Call, method))

                .SearchForward(code => code.opcode == OpCodes.Ldstr)
                .ThrowIfInvalid("Couldn't find LogError")
                .RemoveInstructions(5)

                .InstructionEnumeration();
        }
    }
}
