using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Chat
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

        internal void Log(LogLevel level, string format, params object[] args)
        {
            logger.Log(level, string.Format(format, args));
        }

        internal void LogInfo(string format, params object[] args)
        {
            Log(LogLevel.Info, format, args);
        }
        internal void LogDebug(string format, params object[] args)
        {
            Log(LogLevel.Debug, format, args);
        }
        internal void LogError(string format, params object[] args)
        {
            Log(LogLevel.Error, format, args);
        }
        internal void LogFatal(string format, params object[] args)
        {
            Log(LogLevel.Fatal, format, args);
        }
        internal void LogMessage(string format, params object[] args)
        {
            Log(LogLevel.Message, format, args);
        }
        internal void LogWarning(string format, params object[] args)
        {
            Log(LogLevel.Warning, format, args);
        }
    }
}
