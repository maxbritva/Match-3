using Audio;
using Boot.EntryPoint;
using Cysharp.Threading.Tasks;
using Data;
using Menu.Levels;
using SceneLoading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer.Unity;

namespace Menu
{
    public class MenuEntryPoint: IInitializable
    {
        private GameData _gameData;
        private SceneLoader _sceneLoader;
        private AudioManager _audioManager;
        public LevelsSequence CurrentLevelsSequence { get; private set; } 
        public MenuEntryPoint(GameData gameData, SceneLoader sceneLoader, AudioManager audioManager)
        {
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _audioManager = audioManager;
        }

        public async void Initialize()
        {
            _audioManager.SetSoundVolume();
            await SetupCurentLevels();
        }

        private async UniTask SetupCurentLevels()
        {
            if (_gameData.CurrentLevel <= 5)
                await LoadLevelsSequence("Levels1-5");
            else
                await LoadLevelsSequence("Levels6-10");
        }
        
        private async UniTask LoadLevelsSequence(string key)
        {
            AsyncOperationHandle<LevelsSequence> levels = Addressables.LoadAssetAsync<LevelsSequence>(key);
            await levels.ToUniTask();
                if (levels.Status == AsyncOperationStatus.Succeeded)
                {
                    CurrentLevelsSequence = levels.Result;
                    Addressables.Release(levels);
                }
        }
    }
}