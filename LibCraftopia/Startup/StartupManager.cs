using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using LibCraftopia.Item;
using LibCraftopia.Enchant;
using LibCraftopia.Initialize;
using UnityEngine.UIElements;

namespace LibCraftopia.Startup
{
    public class StartupManager : MonoBehaviour
    {
        private AssetBundle splashSceneBundle;

        private async UniTaskVoid Awake()
        {
            await loadSplashScene();
        }

        private async UniTask loadSplashScene()
        {
            var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var bundlePath = Path.Combine(assemblyDir, "libcraftopia.splash.scene");
            splashSceneBundle = await AssetBundle.LoadFromFileAsync(bundlePath);

            SceneManager.activeSceneChanged += onActiveSceneChanged;
        }

        private void onActiveSceneChanged(Scene current, Scene next)
        {
            transitToSplashScene();
            SceneManager.activeSceneChanged -= onActiveSceneChanged;
        }

        private void transitToSplashScene()
        {
            var sceneName = Path.GetFileNameWithoutExtension(splashSceneBundle.GetAllScenePaths().First());
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            var splashScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(splashScene);
        }

        private async UniTaskVoid Start()
        {
            var document = FindObjectOfType<UIDocument>();
            var progressBar = document.rootVisualElement.Q<ProgressBar>("Progress");
            InitializeManager.Inst.Context.OnValueChanged += (message, progress) =>
            {
                progressBar.title = message;
                progressBar.value = 100 * progress;
            };
            await InitializeManager.Inst.InitAsync();
            var home = SceneManager.GetSceneByBuildIndex(0);
            SceneManager.SetActiveScene(home);
            var sceneName = Path.GetFileNameWithoutExtension(splashSceneBundle.GetAllScenePaths().First());
            await SceneManager.UnloadSceneAsync(sceneName);
            Destroy(this);
        }

        private void OnDestroy()
        {
            splashSceneBundle?.Unload(true);
            splashSceneBundle = null;
        }
    }
}
