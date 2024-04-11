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

            if (DebugMode) {
                UtilsLogger.LogInfo("Debug Mode");

                __instance.m_RoomStats.AddMoney(99999);
            }

            Items.registerItems.ForEach(item =>
            {
                if (!item.purchasable)
                    return;

                ShopItem shopItem = Languages.CreateShopItemWithLanguage(item);

                __instance.m_ItemsForSaleDictionary.Add(shopItem.ItemID, shopItem);

                if (item.Category != ShopItemCategory.Invalid)
                {
                    __instance.m_CategoryItemDic[item.Category].Add(shopItem);
                }
            });
        }
    }
}
