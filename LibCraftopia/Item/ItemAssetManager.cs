using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oc;
using UnityEngine;

namespace LibCraftopia.Item
{
    public class ItemAssetManager : SingletonMonoBehaviour<ItemAssetManager>
    {
        public List<IItemAssetHandler> Handlers { get; private set; } = new List<IItemAssetHandler>();

        protected override void OnUnityAwake()
        {
        }

        internal GameObject LoadItemPrefab(int id)
        {
            var handlers = Handlers.Where((handler) => handler.HasPrefab(id)).ToArray();
            if(handlers.Length > 1) {
                Logger.Inst.LogWarning($"There are several candidates of item asset handlers for id={id}. Use first one.");
            }
            return handlers.First()?.LoadPrefab(id);
        }
    }
}
