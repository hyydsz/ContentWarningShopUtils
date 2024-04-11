using HarmonyLib;
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
            __instance.possibleSpawns = __instance.possibleSpawns
                .Concat(Items.registerItems.Where(item => item.itemType == Item.ItemType.Artifact && item.spawnable))
                .ToArray();
        }
    }
}
