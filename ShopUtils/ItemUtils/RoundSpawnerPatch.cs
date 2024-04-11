using HarmonyLib;
using System.Linq;

namespace ShopUtils.ItemUtils
{
    [HarmonyPatch(typeof(RoundArtifactSpawner))]
    internal class RoundArtifactSpawnerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(RoundArtifactSpawner.SpawnRound))]
        public static void SpawnRound(RoundArtifactSpawner __instance)
        {
            __instance.possibleSpawns = __instance.possibleSpawns
                .Concat(Items.registerSpawnableArtifactItem)
                .ToArray();
        }
    }
}
