using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Chat
{
    public interface IChatCommand
    {
        string Command { get; }

        void Invoke(string[] args);
    }
}
