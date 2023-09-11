using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Initialize
{
    public interface IInitializeHandler
    {
        UniTask Init(InitializeContext context);
    }
}
