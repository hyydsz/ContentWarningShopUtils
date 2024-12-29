using HarmonyLib;
using System.Linq;

namespace ShopUtils.ItemUtils
{
    [HarmonyPatch(typeof(RoundArtifactSpawner))]
    internal static class RoundArtifactSpawnerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(RoundArtifactSpawner), "SpawnRound")]
        private static void SpawnRound(RoundArtifactSpawner __instance)
        {
            __instance.possibleSpawns = __instance.possibleSpawns
                .AddRangeToArray(Items.registerItems.Where(i => i.id != 0 && i.spawnable).ToArray());
        }
    }
}
