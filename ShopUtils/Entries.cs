using ShopUtils.ItemUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ShopUtils
{
    public class Entries
    {
        public static List<Type> registerEntries = new List<Type>();

        ///<summary>
        ///Add ItemDataEntry Type
        /// </summary>
        public static void RegisterEntry(Type type)
        {
            if (!type.IsSubclassOf(typeof(ItemDataEntry)))
            {
                throw new ShopUtilsException($"Unknown Item Data Entry: {type.Name}");
            }

            if (!registerEntries.Contains(type)) {
                registerEntries.Add(type);
            }
        }

        ///<summary>
        ///Remove ItemDataEntry Type
        /// </summary>
        public static void UnRegisterEntry(Type type)
        {
            if (registerEntries.Contains(type)) {
                registerEntries.Remove(type);
            }
        }

        public static Type[] GetTypesFromAssembly(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                UtilsLogger.LogWarning($"ItemDataEntry Register: {assembly} => {ex}");
                return ex.Types.Where(type => type != null).ToArray();
            }
        }

        ///<summary>
        ///Get All Entry From Your Code
        /// </summary>
        public static void RegisterAll(Assembly assembly)
        {
            foreach (Type type in GetTypesFromAssembly(assembly))
            {
                if (type.IsSubclassOf(typeof(ItemDataEntry)))
                {
                    if (!registerEntries.Contains(type))
                    {
                        UtilsLogger.LogInfo($"Found ItemDataEntry: {type.Name}");

                        registerEntries.Add(type);
                    }
                }
            }
        }

        ///<summary>
        ///Get All Entry From Your Code Without Assembly
        /// </summary>
        public static void RegisterAll()
        {
            RegisterAll(Assembly.GetCallingAssembly());
        }
    }
}
