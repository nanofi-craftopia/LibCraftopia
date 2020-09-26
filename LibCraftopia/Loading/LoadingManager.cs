using LibCraftopia.Container;
using LibCraftopia.Hook;
using LibCraftopia.Utils;
using Oc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibCraftopia.Loading
{
    public class LoadingManager : SingletonMonoBehaviour<LoadingManager>
    {
        public PrioritiezedList<int, Func<bool, IEnumerator>> InitializeLoaders { get; }
        public PrioritiezedList<int, Func<bool, IEnumerator>> InitializeGameLoaders { get; }
        public PrioritiezedList<int, Func<bool, IEnumerator>> AfterLoadLoaders { get; }

        public LoadingManager()
        {
            InitializeLoaders = new PrioritiezedList<int, Func<bool, IEnumerator>>();
            InitializeGameLoaders = new PrioritiezedList<int, Func<bool, IEnumerator>>();
            AfterLoadLoaders = new PrioritiezedList<int, Func<bool, IEnumerator>>();
        }

        private bool initialized = false;
        private bool initializedGame = false;

        internal bool IsInitialized
        {
            get { return initialized && initializedGame; }
        }

        protected override void OnUnityAwake()
        {
        }

        internal IEnumerator InvokeInitialize(bool needsStabilization)
        {
            if (!initialized)
            {
                foreach (var item in InitializeLoaders)
                {
                    var loader = item(needsStabilization).LogErrored();
                    while (loader.MoveNext()) yield return loader.Current;

                }
                initialized = true;
            }
            if (!initializedGame)
            {
                foreach (var item in InitializeGameLoaders)
                {
                    var loader = item(needsStabilization).LogErrored();
                    while (loader.MoveNext()) yield return loader.Current;

                }
                initializedGame = true;
            }
        }

        internal IEnumerator InvokeAfterLoad(bool needsStabilization)
        {
            foreach (var item in AfterLoadLoaders)
            {
                var loader = item(needsStabilization).LogErrored();
                while (loader.MoveNext()) yield return loader.Current;
            }
        }

        internal void OnGameSceneUnloaded()
        {
            initializedGame = false;
        }
    }
}
