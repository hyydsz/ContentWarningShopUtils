using Steamworks;
using System;

namespace ShopUtils.Network
{
    public class Networks
    {
        // Actually, I was originally planning to use the MyceliumNetwork mods
        // But it is not possible to set keys when creating lobby :(

        private static CSteamID steamID;

        private static bool LobbyCreated = false;
        private static bool inLobby = false;

        public static Action OnLobbyCreated;
        public static Action OnLobbyEnter;
        public static Action OnLobbyLeave;
        public static Action OnLobbyCreateFailure;

        internal static void InitNetwork()
        {
            Callback<LobbyCreated_t>.Create(m_LobbyCreated);
            Callback<LobbyEnter_t>.Create(m_LobbyEnter);
        }

        private static void m_LobbyCreated(LobbyCreated_t lobby)
        {
            if (lobby.m_eResult == EResult.k_EResultOK)
            {
                steamID = new CSteamID(lobby.m_ulSteamIDLobby);

                LobbyCreated = true;

                UtilsLogger.LogInfo("LobbyCreated.");
                OnLobbyCreated?.Invoke();
            }
            else
            {
                OnLobbyCreateFailure?.Invoke();
            }
        }

        private static void m_LobbyEnter(LobbyEnter_t lobby)
        {
            steamID = new CSteamID(lobby.m_ulSteamIDLobby);

            inLobby = true;

            UtilsLogger.LogInfo("LobbyEntered.");
            OnLobbyEnter?.Invoke();
        }

        internal static void m_LobbyLeave()
        {
            steamID.Clear();

            LobbyCreated = false;
            inLobby = false;    

            OnLobbyLeave?.Invoke();
        }

        public static void RegisterItemPrice(Item item)
        {
            string itemName = "Item" + "-" + item.persistentID;

            OnLobbyCreated += () =>
            {
                SetLobbyData(itemName, item.price.ToString());
            };

            OnLobbyEnter += () =>
            {
                if (int.TryParse(GetLobbyData(itemName), out int price))
                {
                    item.price = price;
                    return;
                };

                UtilsLogger.LogError($"Sync Item Price Error. Item: {item.displayName}");
            };
        }

        public static void SetLobbyData(string key, object value)
        {
            if (!LobbyCreated)
            {
                UtilsLogger.LogError("Cannot set lobby data when not create lobby");
                return;
            }

            if (!SteamMatchmaking.SetLobbyData(steamID, key, value.ToString()))
            {
                UtilsLogger.LogError($"Set lobby key Failure. Key: {key}");
            }
        }

        ///<summary>
        ///Only return string
        /// </summary>
        public static string GetLobbyData(string key)
        {
            if (!inLobby)
            {
                UtilsLogger.LogError("Cannot get lobby data when not in lobby");
                return null;
            }

            return SteamMatchmaking.GetLobbyData(steamID, key);
        }
    }
}
