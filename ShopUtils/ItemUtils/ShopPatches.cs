using HarmonyLib;
using ShopUtils;

namespace Boombox.ItemUtils
{
    [HarmonyPatch(typeof(ShopHandler))]
    internal static class ShopPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(ShopHandler.InitShop))]
        private static void InitShop(ShopHandler __instance)
        {
            UtilsLogger.LogInfo("Shop Begin Init");

            Items.registerItems.ForEach(item =>
            {
                if (!item.purchasable)
                    return;

                ShopItem shopItem = new ShopItem(item);

                __instance.m_ItemsForSaleDictionary.Add(shopItem.ItemID, shopItem);
                if (item.Category != ShopItemCategory.Invalid)
                {
                    __instance.m_CategoryItemDic[item.Category].Add(shopItem);
                }
            });
        }
    }
}
