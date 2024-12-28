using HarmonyLib;
using System.Linq;
using Zorro.Core;

namespace ShopUtils.ItemUtils
{
    [HarmonyPatch(typeof(GameHandler))]
    internal static class GameHandlerPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch("Initialize")]
        private static void Initialize()
        {
            Items.InitAllItems();

            ItemDatabase item = SingletonAsset<ItemDatabase>.Instance;
            item.Objects.AddRange(Items.registerItems.Where(i => i.id != 0));
        }
    }
}
