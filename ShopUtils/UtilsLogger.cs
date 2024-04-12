using BepInEx.Logging;
using System;

namespace ShopUtils
{
    public static class UtilsLogger
    {
        private static ManualLogSource logger;

        internal static void InitLogger(ManualLogSource souce)
        {
            logger = souce;
        }

        public static void LogInfo(string message)
        {
            logger.LogInfo(message);
        }

        public static void LogError(string message)
        {
            logger.LogError(message);
        }

        public static void LogWarning(string message)
        {
            logger.LogWarning(message);
        }
    }

    public class ShopUtilsException : Exception
    {
        public ShopUtilsException(string message) : base(message) { }
    }
}
