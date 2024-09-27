using Data;
using DG.Tweening;
using SceneLoading;
using UnityEngine;
using VContainer.Unity;

namespace Boot.EntryPoint
{
    public class BootEntryPoint: IInitializable
    {
        private GameData _gameData;
        private IAsyncSceneLoading _sceneLoader;
       
        private BootEntryPoint(IAsyncSceneLoading sceneLoader, GameData gameData)
        {
            _sceneLoader = sceneLoader;
            _gameData = gameData;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);
            await _sceneLoader.LoadAsync(Scenes.MENU);
            _sceneLoader.LoadingIsDone(true);
        }
    }
}