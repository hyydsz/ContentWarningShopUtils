using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

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

    [HarmonyPatch(typeof(RoundArtifactSpawner))]
    internal class RoundArtifactSpawnerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(RoundArtifactSpawner.SpawnRound))]
        public static void SpawnRound(RoundArtifactSpawner __instance)
        {
            List<Item> items = new List<Item>();

            foreach (Item item in Items.registerItems)
            {
                if (item.itemType == Item.ItemType.Artifact && item.spawnable)
                {
                    items.Add(item);
                }
            }

            __instance.possibleSpawns = __instance.possibleSpawns.Concat(items).ToArray();
        }
    }
}
