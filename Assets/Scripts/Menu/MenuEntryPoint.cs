using Audio;
using Boot.EntryPoint;
using Data;
using SceneLoading;
using UnityEngine;
using VContainer.Unity;

namespace Menu
{
    public class MenuEntryPoint: IInitializable
    {
        private GameData _gameData;
        private SceneLoader _sceneLoader;
        private AudioManager _audioManager;

        public MenuEntryPoint(GameData gameData, SceneLoader sceneLoader, AudioManager audioManager)
        {
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _audioManager = audioManager;
        }

        public void Initialize()
        {
            _audioManager.SetSoundVolume();
         
        }
    }
}