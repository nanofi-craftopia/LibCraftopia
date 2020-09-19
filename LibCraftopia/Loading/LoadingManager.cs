using Oc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Loading
{
    public class LoadingManager : SingletonMonoBehaviour<LoadingManager>
    {
        public SortedList<int, Func<bool, IEnumerator>> InitializeLoaders { get; private set; } = new SortedList<int, Func<bool, IEnumerator>>();
        public SortedList<int, Func<bool, IEnumerator>> InitializeGameLoaders { get; private set; } = new SortedList<int, Func<bool, IEnumerator>>();

        private bool initialized = false;
        private bool initializedGame = false;

        protected override void OnUnityAwake()
        {
        }

        internal IEnumerator OnLoadScene(bool needsStabilization)
        {
            if (!initialized)
            {
                foreach (var item in InitializeLoaders.Values)
                {
                    var loader = item(needsStabilization);
                    while (loader.MoveNext())
                    {
                        yield return loader.Current;
                    }

                }
                initialized = true;
            }
            if (!initializedGame)
            {
                foreach (var item in InitializeGameLoaders.Values)
                {
                    var loader = item(needsStabilization);
                    while (loader.MoveNext())
                    {
                        yield return loader.Current;
                    }

                }
                initializedGame = true;
            }
        }

        internal void OnGameSceneUnloaded()
        {
            initializedGame = false;
        }
    }
}
