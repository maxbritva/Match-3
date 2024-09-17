using Cysharp.Threading.Tasks;
using Data;
using SceneLoading;
using UnityEngine;
using VContainer.Unity;

namespace Boot.EntryPoint
{
    public class BootEntryPoint: IInitializable
    {
        public GameData GameData { get; private set; }
        private SceneLoader _sceneLoader;
       
        private BootEntryPoint(SceneLoader sceneLoader, GameData gameData)
        {
            _sceneLoader = sceneLoader;
            GameData = gameData;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            await LoadScene(Scenes.MENU);
        }
        private async UniTask LoadScene(string sceneName) => await _sceneLoader.LoadAsync(sceneName);

        // private async UniTask SetScreenLoadingPrefab()
        // {
        //     AsyncOperationHandle<GameObject> prefabUI = Addressables.LoadAssetAsync<GameObject>("LoadingScreenUI");
        //     await prefabUI.ToUniTask();
        //         if (prefabUI.Status == AsyncOperationStatus.Succeeded)
        //         {
        //             var prefab = Object.Instantiate(prefabUI.Result);
        //             Object.DontDestroyOnLoad(prefab.gameObject);
        //             _sceneLoadingView = prefab.GetComponent<SceneLoadingView>();
        //             Addressables.Release(prefabUI);
        //         }
        // }
    }
}