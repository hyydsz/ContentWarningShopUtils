using BepInEx;
using HarmonyLib;

namespace ShopUtils
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class PluginInfo : BaseUnityPlugin
    {
        public const string ModGUID = "WaterEgg-ShopUtils";
        public const string ModName = "ShopUtils";
        public const string ModVersion = "1.0.0";

        private Harmony harmony = new Harmony(ModGUID);

        void Awake()
        {
            UtilsLogger.InitLogger(Logger);

            harmony.PatchAll();
        }
    }
}
