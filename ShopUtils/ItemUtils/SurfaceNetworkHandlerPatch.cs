using HarmonyLib;

namespace ShopUtils.ItemUtils
{
    [HarmonyPatch(typeof(SurfaceNetworkHandler))]
    public class SurfaceNetworkHandlerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("InitSurface")]
        public static void InitSurface()
        {
            Items.InitAllItems();
        }
    }
}
