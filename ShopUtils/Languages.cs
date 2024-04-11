using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace ShopUtils
{
    public class Languages
    {
        internal static Dictionary<string, Dictionary<Locale, string>> languages = new Dictionary<string, Dictionary<Locale, string>>();

        public static void AddLanguage(string name, string language, Locale locale)
        {
            if (!languages.ContainsKey(name))
            {
                languages.Add(name, new Dictionary<Locale, string>());
            }

            languages[name].Add(locale, language);
        }

        public static Locale GetLanguage(string name)
        {
            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                if (locale.LocaleName.Contains(name))
                {
                    return locale;
                }
            }

            return null;
        }

        public static void DebugLanguage()
        {
            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                UtilsLogger.LogInfo($"Language: {locale.LocaleName}");
            }
        }

        public static ShopItem CreateShopItemWithLanguage(Item item)
        {
            // I Can't Patch ShopItem::DisplayName
            // It's private set only!

            string displayName = item.displayName;

            /* ==================================================================== */
            string text = item.name.Trim().Replace(" ", "");
            if (languages.TryGetValue(text, out var language))
            {
                if (language.ContainsKey(LocalizationSettings.SelectedLocale))
                {
                    item.displayName = language[LocalizationSettings.SelectedLocale];
                }
            }

            ShopItem shop = new ShopItem(item);
            /* ==================================================================== */

            item.displayName = displayName;

            return shop;
        }
    }
}
