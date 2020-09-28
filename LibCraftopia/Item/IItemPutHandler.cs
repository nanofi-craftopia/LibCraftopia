using Oc;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Item
{
    public interface IItemPutHandler
    {
        bool IsPuttable();
        void OnPut(OcPlEquip equip);
        void OnKilled(OcPlEquip equip);
    }
}
