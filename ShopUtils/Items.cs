using ShopUtils.ItemUtils;
using System;
using System.Collections.Generic;
using Zorro.Core;

namespace ShopUtils
{
    public class Items
    {
        public static List<Item> registerItems = new List<Item>();

        ///<summary>
        ///Add Item Into Shop
        /// </summary>
        public static void RegisterItem(Item item, ShopItemCategory category, int price)
        {
            if (price >= 0)
            {
                item.price = price;
            }

            if (category != ShopItemCategory.Invalid)
            {
                item.Category = category;
            }

            if (item.Category == ShopItemCategory.Invalid)
            {
                throw new ShopUtilsException(
                    $"Error Category Invalid \n" +
                    $"Items Name: {item.displayName} \n"
                    );
            }

            if (item.price < 0)
            {
                throw new ShopUtilsException(
                    $"Error Item Price \n" +
                    $"Items Name: {item.displayName} \n"
                    );
            }

            registerItems.Add(item);
        }

        ///<summary>
        ///Add Item Into Shop
        /// </summary>
        public static void RegisterItem(Item item, ShopItemCategory category)
        {
            RegisterItem(item, category, -1);
        }

        ///<summary>
        ///Add Item Into Shop
        /// </summary>
        public static void RegisterItem(Item item, int price)
        {
            RegisterItem(item, ShopItemCategory.Invalid, price);
        }

        ///<summary>
        ///Add Item Into Shop
        /// </summary>
        public static void RegisterItem(Item item)
        {
            RegisterItem(item, ShopItemCategory.Invalid, -1);
        }

        ///<summary>
        ///Remove Item From Shop
        /// </summary>
        public static void UnRegisterItem(Item item)
        {
            if (registerItems.Contains(item)) {
                registerItems.Remove(item);
            }

        }

        ///<summary>
        ///Init All Items Id and Guid
        /// </summary>
        public static void InitAllItems()
        {
            registerItems.ForEach(item => item.id = 0);
            registerItems.ForEach(item =>
            {
                item.id = GetMaxItemID();

                UtilsLogger.LogInfo($"Item: {item.displayName}, ItemId: {item.id}, Guid: {item.persistentID}");
            });
        }

        private static byte GetMaxItemID()
        {
            int id = 0;
            foreach (Item item in SingletonAsset<ItemDatabase>.Instance.Objects)
            {
                id = Math.Max(item.id, id);
            }

            foreach (Item item in registerItems)
            {
                id = Math.Max(item.id, id);
            }

            return (byte) (id + 1);
        }
    }
}
