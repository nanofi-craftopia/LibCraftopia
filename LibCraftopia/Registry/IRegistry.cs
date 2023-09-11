using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Registry
{    public interface IRegistry
    {
        string Name { get; }
        UniTask Init();
        UniTask Apply();

        UniTask Load(string baseDir);
        UniTask Save(string baseDir);
    }
}
