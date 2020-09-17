using HarmonyLib;
using Oc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Chat
{
    static class ChatHandlerExtensions
    {
        public static void AddMessage(this OcUI_ChatHandler self, string message)
        {
            // See OcUI_ChatHandler.PopMessage
            var messageList = AccessTools.FieldRefAccess<OcUI_ChatHandler, OcUI_ChatMessage[]>(self, "messageList");
            ref var nextMessage = ref AccessTools.FieldRefAccess<OcUI_ChatHandler, int>(self, "nextMessage");
            if(messageList[nextMessage] == null)
            {
                var prefabSheet = AccessTools.FieldRefAccess<OcUI_ChatHandler, OcUI_ChatMessage>(self, "pf_messageSheet");
                var parent = AccessTools.FieldRefAccess<OcUI_ChatHandler, Transform>(self, "messageParent");
                messageList[nextMessage] = prefabSheet.CreateInst(parent, false);
            }
            var curMessage = messageList[nextMessage];
            curMessage.Init(-1, "Command", message); // netId=-1 means a message with the default color (black)
            curMessage.transform.SetAsLastSibling();
            nextMessage++;
            if(nextMessage >= 6)
            {
                nextMessage -= 6;
            }
            // skip to show bubble message
        }
    }

    public class ChatCommandManager : SingletonMonoBehaviour<ChatCommandManager>
    {
        private static readonly MethodInfo infoPopMessage = AccessTools.Method(typeof(OcUI_ChatHandler), "PopMessage");
        public ChatCommandDictionary Commands { get; private set; } = new ChatCommandDictionary();

        protected override void OnUnityAwake()
        {
        }

        public void PopMessage(string format, params object[] args)
        {
            var chatHandler = OcUI_ChatHandler.Inst;
            if (chatHandler != null)
            {
                var message = string.Format(format, args);
                chatHandler.AddMessage(message);
            }
        }


        public ChatCommandHandler FindCommand(string message)
        {
            var tokens = parse(message).ToList();
            if (tokens.Count == 0)
                return null;
            var command = Commands.GetOrDefault(tokens[0]);
            if (command == null)
                return null;
            var i = 1;
            while (i < tokens.Count && command is IChatCommandWithSubs subs)
            {
                var sub = subs.Subcommand(tokens[i]);
                if (sub == null)
                {
                    break;
                }
                command = sub;
                i++;
            }
            var args = tokens.Skip(i).ToArray();
            return new ChatCommandHandler(command, args);
        }

        private IEnumerable<string> parse(string message)
        {
            int i = 1;
            while (i < message.Length)
            {
                var cur = message[i];
                if (cur == '"' || cur == '\'')
                {
                    var end = cur;
                    var builder = new StringBuilder();
                    i++;
                    var successed = false;
                    while (i < message.Length)
                    {
                        var ch = message[i];
                        if (ch == '\\')
                        {
                            if (++i < message.Length && message[i] == end)
                            {
                                builder.Append(end);
                                i++;
                            }
                            else
                            {
                                builder.Append(ch);
                            }
                        }
                        else if (ch == end)
                        {
                            i++;
                            if (i < message.Length && !char.IsWhiteSpace(message[i]))
                            {
                                throw new Exception("A space must follow after end of quotation.");
                            }
                            while (i < message.Length && char.IsWhiteSpace(message[i]))
                            {
                                i++;
                            }
                            successed = true;
                            break;
                        }
                        else
                        {
                            builder.Append(ch);
                        }
                        i++;
                    }
                    if (!successed)
                    {
                        throw new Exception("The quotation must end.");
                    }
                    yield return builder.ToString();
                }
                else
                {
                    var builder = new StringBuilder();
                    while (i < message.Length)
                    {
                        var ch = message[i];
                        if (char.IsWhiteSpace(ch))
                        {
                            break;
                        }
                        else
                        {
                            builder.Append(ch);
                        }
                        i++;
                    }
                    while (i < message.Length && char.IsWhiteSpace(message[i]))
                    {
                        i++;
                    }
                    yield return builder.ToString();
                }
            }
        }
    }
}
