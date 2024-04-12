using HarmonyLib;

namespace ShopUtils.ItemUtils
{
    [HarmonyPatch(typeof(SurfaceNetworkHandler))]
    internal static class SurfaceNetworkHandlerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch("InitSurface")]
        public static void InitSurface()
        {
            Items.InitAllItems();
        }
    }
}
