using UnityEngine;
using System.Collections;
namespace ffDevelopmentSpace
{
    public class Debuger
    {

        private static bool IsDebug = true;

        public static bool IsDebuger
        { get { return IsDebug; }
          set { IsDebug = value; } }

        public static void Log(object message)
        {
            if (IsDebug)
            {
                Debug.Log(message);
                Singleton<ModuleEventDispatcher>.GetInstance().DispatchDebugLogEvent(message.ToString());
            }
        }

        public static void LogWarning(object message)
        {
            if (IsDebug)
                Debug.LogWarning(message);
        }

        public static void LogError(object message)
        {
            if (IsDebug)
                Debug.LogError(message);
        }
    }
}
