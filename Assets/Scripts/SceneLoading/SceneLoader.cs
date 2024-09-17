using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public class SceneLoader: IAsyncSceneLoading
    {
        private readonly Dictionary<string, SceneInstance> _loadedScenes = new Dictionary<string, SceneInstance>();
        
        public async UniTask LoadAsync(string sceneName)
        {
            var loadedScene = await Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive).ToUniTask();
            _loadedScenes.Add(sceneName, loadedScene);
        }

        public async UniTask UnloadAsync(string sceneName)
        {
            var sceneToUnload = _loadedScenes[sceneName];
            await Addressables.UnloadSceneAsync(sceneToUnload).ToUniTask();
        }
    }
}