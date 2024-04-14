using BepInEx;
using HarmonyLib;
using ShopUtils.Network;

namespace ShopUtils
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class PluginInfo : BaseUnityPlugin
    {
        public const string ModGUID = "hyydsz-ShopUtils";
        public const string ModName = "ShopUtils";
        public const string ModVersion = "1.0.8";

        private Harmony harmony = new Harmony(ModGUID);

        void Awake()
        {
            UtilsLogger.InitLogger(Logger);
            Entries.InitEntryCount();
            Networks.InitNetwork();

            harmony.PatchAll();
        }
    }
}
