using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Chat
{
    public interface IChatCommandWithSubs : IChatCommand
    {
        IChatCommand Subcommand(string command);
    }
}
