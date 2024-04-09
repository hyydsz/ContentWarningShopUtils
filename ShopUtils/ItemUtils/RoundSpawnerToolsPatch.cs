using HarmonyLib;

namespace ShopUtils.ItemUtils
{
    [HarmonyPatch(typeof(RoundSpawnerTools))]
    internal class RoundSpawnerToolsPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(RoundSpawnerTools.Populate))]
        public static void Populate(RoundSpawnerTools __instance)
        {
            foreach (Item item in Items.registerItems)
            {
                if (item.itemType == Item.ItemType.Tool && item.spawnable)
                {
                    __instance.possibleSpawns.Add(item);
                }
            }
        }
    }
}
