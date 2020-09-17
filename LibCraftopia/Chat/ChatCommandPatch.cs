using HarmonyLib;
using Oc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LibCraftopia.Chat
{
    public class ChatCommandPatch
    {
        private static readonly MethodInfo infoEndEnterMessage = AccessTools.Method(typeof(OcUI_ChatHandler), "EndEnterMessage");


        [HarmonyPatch(typeof(OcUI_ChatHandler), "TrySendMessage")]
        [HarmonyPrefix]
        public static bool PrefixTrySendMessage(OcUI_ChatHandler __instance, string message)
        {
            Logger.Inst.LogInfo("TrySendMessage called: {0}", message);
            message = message?.Trim();
            if (message.IsNullOrEmpty())
                return true;
            if (!message.StartsWith("/"))
                return true;
            try
            {
                var command = ChatCommandManager.Inst.FindCommand(message);
                if (command != null)
                {
                    infoEndEnterMessage.Invoke(__instance, Array.Empty<object>());
                    command.Invoke();
                }
                return command == null;
            }
            catch (Exception e)
            {
                ChatCommandManager.Inst.PopMessage(e.Message);
                return false;
            }

        }
    }
}
