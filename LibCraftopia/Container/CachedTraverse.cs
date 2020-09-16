using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Container
{
    public delegate void OnCacheChangedHandler<T>(CachedTraverse<T> sender, T oldValue, T newValue) where T : class;
    public class CachedTraverse<T> where T : class
    {
        private readonly Traverse target;
        private T cachedValue;

        public CachedTraverse(Traverse target)
        {
            this.target = target;
        }

        private event OnCacheChangedHandler<T> changed;
        public event OnCacheChangedHandler<T> Changed
        {
            add { changed += value; }
            remove { changed -= value; }
        }

        public Traverse Traget
        {
            get { return target; }
        }

        public T Value
        {
            get
            {
                Cache();
                return cachedValue;
            }
            set
            {
                if (value != cachedValue)
                {
                    changed?.Invoke(this, cachedValue, value);
                    target.SetValue(value);
                    cachedValue = value;
                }
            }
        }

        public void Cache()
        {
            var newValue = target.GetValue<T>();
            if (newValue != cachedValue)
            {
                changed?.Invoke(this, cachedValue, newValue);
                cachedValue = newValue;
            }
        }
    }
}
