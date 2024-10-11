namespace YGame.Scripts.Log
{
    public class YLogger
    {
        private const string LOG = "YLogger INFO :: ";
        private const string WARNING = "YLogger  WARNING :: ";
        private const string ERROR = "YLogger ERROR :: ";
        public static bool IsDebugEnabled { get; set; }
        
        public static void LogInfo(string message)
        {
            if (!IsDebugEnabled)
                return;
            var log = $"{LOG}{message}";
            UnityEngine.Debug.Log(log);
        }
        
        public static void LogWarning(string message)
        {
            if (!IsDebugEnabled)
                return;
            var warning = $"{WARNING}{message}";
            UnityEngine.Debug.LogWarning(warning);
        }

        public static void LogError(string message)
        {
            if (!IsDebugEnabled)
                return;
            var error = $"{ERROR}{message}";
            UnityEngine.Debug.LogError(error);
        }
    }
}