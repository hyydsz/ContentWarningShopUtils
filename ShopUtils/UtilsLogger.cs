using BepInEx.Logging;

namespace ShopUtils
{
    public class UtilsLogger
    {
        private static ManualLogSource logger;

        public static void InitLogger(ManualLogSource souce)
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
}
