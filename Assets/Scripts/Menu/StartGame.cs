using Audio;
using Data;
using Level;
using SceneLoading;

namespace Menu
{
    public class StartGame
    {
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _sceneLoader;

        public StartGame(GameData gameData, IAsyncSceneLoading sceneLoader, AudioManager audioManager)
        {
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _audioManager = audioManager;
        }

        public async void Start(LevelConfiguration levelToStart)
        {
            _gameData.SetCurrentLevel(levelToStart);
            await _sceneLoader.UnloadAsync(Scenes.MENU);
            _audioManager.PlayGameMusic();
            await  _sceneLoader.LoadAsync(Scenes.GAME);
        }
    }
}