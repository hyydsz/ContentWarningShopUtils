using ShopUtils.Language;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace ShopUtils
{
    public static class Languages
    {
        // Language <key, value>
        internal static Dictionary<Locale, Dictionary<string, string>> languages = new Dictionary<Locale, Dictionary<string, string>>();

        public static void AddLanguage(string key, string language, Locale locale)
        {
            if (locale == null) {
                throw new ShopUtilsException($"Locale is null, key: {key}");
            }

            if (!languages.ContainsKey(locale)) {
                languages.Add(locale, new Dictionary<string, string>());
            }

            if (languages[locale].ContainsKey(key))
            {
                UtilsLogger.LogError($"Language: {locale.LocaleName} Has Already Key: {key}");
                return;
            }

            languages[locale].Add(key, language);
        }

        public static void AddLanguage(this Locale locale, string key, string language)
        {
            AddLanguage(key, language, locale);
        }

        public static void AddLanguage(this Locale locale, params LanguageInstance[] languages)
        {
            foreach (LanguageInstance language in languages)
            {
                locale.AddLanguage(language.key, language.language);
            }
        }

        public static Locale GetLanguage(LanguageEnum language)
        {
            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                string str = language.ToString().Replace("Chinese", "");

                if (locale.LocaleName.Contains(str)) {
                    return locale;
                }
            }

            return null;
        }

        public static bool TryGetLanguage(string key, out string language)
        {
            if (languages.TryGetValue(LocalizationSettings.SelectedLocale, out var lang))
            {
                if (lang.ContainsKey(key))
                {
                    language = lang[key];
                    return true;
                }
            }

            language = null;
            return false;
        }

        public static void DebugLanguage()
        {
            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                UtilsLogger.LogInfo($"Language: {locale.LocaleName}");
            }
        }
    }

    public class LanguageInstance
    {
        public string key;
        public string language;

        public LanguageInstance(string key, string language)
        {
            this.key = key;
            this.language = language;
        }
    }
}
