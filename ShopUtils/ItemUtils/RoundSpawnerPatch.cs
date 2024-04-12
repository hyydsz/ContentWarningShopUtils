using HarmonyLib;
using System.Linq;

namespace ShopUtils.ItemUtils
{
    [HarmonyPatch(typeof(RoundArtifactSpawner))]
    internal static class RoundArtifactSpawnerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(RoundArtifactSpawner.SpawnRound))]
        private static void SpawnRound(RoundArtifactSpawner __instance)
        {
            __instance.possibleSpawns = __instance.possibleSpawns
                .Concat(Items.registerSpawnableItem)
                .ToArray();
        }
    }
}
