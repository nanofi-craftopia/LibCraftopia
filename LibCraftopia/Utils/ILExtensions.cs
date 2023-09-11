using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LibCraftopia.Utils
{
    public static class ILExtensions
    {
        public static CodeMatcher Replace(this CodeMatcher self, Func<CodeInstruction, CodeInstruction> converter)
        {
            var converted = converter(self.Instruction);
            return self.SetInstruction(converted);
        }
    }
}
