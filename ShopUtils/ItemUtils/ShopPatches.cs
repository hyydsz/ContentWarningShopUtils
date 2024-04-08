using HarmonyLib;
using ShopUtils;

namespace Boombox.ItemUtils
{
    [HarmonyPatch(typeof(ShopHandler))]
    public class ShopPatches
    {
        public static bool DebugMode = false;

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ShopHandler.InitShop))]
        public static void InitShop(ShopHandler __instance)
        {
            UtilsLogger.LogInfo("Shop Begin Init");

            Items.registerItems.ForEach(item =>
            {
                int price = item.price;

                if (DebugMode) {
                    item.price = 0;
                }

                ShopItem shopItem = new ShopItem(item);
                item.price = price;

                __instance.m_ItemsForSaleDictionary.Add(shopItem.ItemID, shopItem);

                if (item.Category != ShopItemCategory.Invalid)
                {
                    __instance.m_CategoryItemDic[item.Category].Add(shopItem);
                }
            });
        }
    }
}
