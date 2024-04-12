using HarmonyLib;
using ShopUtils;
using System;

namespace Boombox.ItemUtils
{
    [HarmonyPatch(typeof(ItemDatabase))]
    internal static class ItemDatabasePatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(ItemDatabase.TryGetItemFromID))]
        private static void TryGetItemFromID(ref bool __result, byte id, ref Item item)
        {
            if (!__result)
            {
                foreach (Item item1 in Items.registerItems)
                {
                    if (item1.id == id)
                    {
                        item = item1;
                        __result = true;

                        break;
                    }
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ItemDatabase.TryGetItemFromPersistentID))]
        private static void TryGetItemFromPersistentID(ref bool __result, Guid id, ref Item item)
        {
            if (!__result)
            {
                foreach (Item item1 in Items.registerItems)
                {
                    if (item1.PersistentID == id)
                    {
                        item = item1;
                        __result = true;

                        break;
                    }
                }
            }
        }
    }
}
