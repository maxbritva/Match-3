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
        
        public async UniTask LoadAsync(Scene scene)
        {
            var loadedScene = await Addressables.LoadSceneAsync(scene.name, LoadSceneMode.Additive).ToUniTask();
            _loadedScenes.Add(scene.name, loadedScene);
        }

        public async UniTask UnloadAsync(Scene scene)
        {
            var sceneToUnload = _loadedScenes[scene.name];
            await Addressables.UnloadSceneAsync(sceneToUnload).ToUniTask();
        }
    }
}