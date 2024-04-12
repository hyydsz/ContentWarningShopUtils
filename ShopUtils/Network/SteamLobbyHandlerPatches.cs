using HarmonyLib;

namespace ShopUtils.Network
{
    [HarmonyPatch(typeof(SteamLobbyHandler))]
    internal static class SteamLobbyHandlerPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(SteamLobbyHandler.LeaveLobby))]
        private static void LeaveLobby()
        {
            Networks.m_LobbyLeave();
        }
    }
}
