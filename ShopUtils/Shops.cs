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
        ///Give you 99999 Money
        /// </summary>
        public static void DebugMode()
        {
            ShopPatches.DebugMode = true;
        }
    }
}
