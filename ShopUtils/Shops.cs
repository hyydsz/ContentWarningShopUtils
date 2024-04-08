using Boombox.ItemUtils;

namespace ShopUtils
{
    public class Shops
    {
        ///<summary>
        ///Need Surface Initialized
        /// </summary>
        public static void OpenShop()
        {
            if (ShopHandler.Instance)
            {
                ShopViewScreen screen = ShopHandler.Instance.m_ShopView;
                screen.DrawCategories();
                screen.InitListeners();
                screen.UpdateViewScreen();
                screen.OnUpdate();
            }
        }

        ///<summary>
        ///Need Surface Initialized
        /// </summary>
        public static void CloseShop()
        {
            if (ShopHandler.Instance)
            {
                ShopViewScreen screen = ShopHandler.Instance.m_ShopView;
                screen.DestroyItemGrid();
                screen.DestroyCategoryGrid();
                screen.CloseShop();
            }
        }

        ///<summary>
        ///Call this When you Start Game 
        ///Set All Items Price to 0
        /// </summary>
        public static void DebugMode()
        {
            UtilsLogger.LogInfo("Debug Mode");

            ShopPatches.DebugMode = true;
        }
    }
}
