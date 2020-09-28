using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Utils
{
    public static class PrefabExtensions
    {
        private static GameObject parent;

        private static GameObject Parent
        {
            get
            {
                if (!parent)
                {
                    parent = new GameObject("LibCraftopia.PrefabParent");
                    UnityEngine.Object.DontDestroyOnLoad(parent);
                    parent.SetActive(false);
                }
                return parent;
            }
        }

        public static T InstantiateClone<T>(this T original, string name) where T : UnityEngine.Object
        {
            var prefab = UnityEngine.Object.Instantiate(original, Parent.transform);
            prefab.name = name;
            return prefab;
        }
    }
}
