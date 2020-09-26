using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia
{
    internal class Logger : SingletonMonoBehaviour<Logger>
    {
        private ManualLogSource logger;

        protected override void OnUnityAwake()
        {
        }

        internal void Init(ManualLogSource logger)
        {
            this.logger = logger;
        }

        internal void Log(LogLevel level, object data)
        {
            logger.Log(level, data);
        }

        internal void LogInfo(object data)
        {
            Log(LogLevel.Info, data);
        }
        internal void LogDebug(object data)
        {
            Log(LogLevel.Debug, data);
        }
        internal void LogError(object data)
        {
            Log(LogLevel.Error, data);
        }
        internal void LogFatal(object data)
        {
            Log(LogLevel.Fatal, data);
        }
        internal void LogMessage(object data)
        {
            Log(LogLevel.Message, data);
        }
        internal void LogWarning(object data)
        {
            Log(LogLevel.Warning, data);
        }

        internal void LogException(Exception e)
        {
            LogError(e);
            LogError(e.Message);
            LogError(e.StackTrace);
        }
    }
}
