using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Chat
{
    public class ChatCommandHandler
    {
        private IChatCommand command;
        private string[] args;
        public ChatCommandHandler(IChatCommand command, string[] args)
        {
            this.command = command;
            this.args = args;
        }

        public void Invoke()
        {
            command.Invoke(args);
        }
    }
}
