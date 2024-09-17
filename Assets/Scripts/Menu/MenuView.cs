using System;
using SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu
{
    public class MenuView: MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;

        private SceneLoader _sceneLoader;
        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
        }

        private async void StartGame()
        {
           await _sceneLoader.UnloadAsync(Scenes.MENU);
          await  _sceneLoader.LoadAsync(Scenes.GAME);
        }

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
    }
}