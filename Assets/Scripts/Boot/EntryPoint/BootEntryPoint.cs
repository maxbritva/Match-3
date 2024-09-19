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
            await _sceneLoader.LoadAsync(Scenes.MENU);
        }
    }
}