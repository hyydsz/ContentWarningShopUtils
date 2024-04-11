using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;

namespace ShopUtils.Language
{
    [HarmonyPatch(typeof(Item))]
    public class LanguagePatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Item.GetTootipData))]
        public static bool GetTootipData(Item __instance, ref IEnumerable<IHaveUIData> __result)
        {
            if (Items.registerItems.Contains(__instance))
            {
                if (__instance.Tooltips.Count > 0)
                {
                    string text = __instance.name.Trim().Replace(" ", "") + "_ToolTips";
                    if (Languages.languages.TryGetValue(text, out var language))
                    {
                        if (language.ContainsKey(LocalizationSettings.SelectedLocale))
                        {
                            string[] array = language[LocalizationSettings.SelectedLocale].Split(';');

                            __instance.Tooltips = new List<ItemKeyTooltip>();
                            foreach (string s in array)
                            {
                                __instance.Tooltips.Add(new ItemKeyTooltip(s));
                            }
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
        public static bool GetLocalizedDisplayName(Item __instance, ref string __result)
        {
            if (Items.registerItems.Contains(__instance))
            {
                string text = __instance.name.Trim().Replace(" ", "");
                if (Languages.languages.TryGetValue(text, out var language))
                {
                    if (language.ContainsKey(LocalizationSettings.SelectedLocale))
                    {
                        __result = language[LocalizationSettings.SelectedLocale];
                        return false;
                    }
                }

                __result = __instance.displayName;
                return false;
            }

            return true;
        }
    }
}
