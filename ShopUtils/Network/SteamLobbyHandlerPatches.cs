using HarmonyLib;

namespace ShopUtils.Network
{
    [HarmonyPatch(typeof(SteamLobbyHandler))]
    public class SteamLobbyHandlerPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(SteamLobbyHandler.LeaveLobby))]
        public static void LeaveLobby()
        {
            Networks.m_LobbyLeave();
        }
    }
}
