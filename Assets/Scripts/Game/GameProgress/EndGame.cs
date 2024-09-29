using Audio;
using Data;
using SceneLoading;

namespace Game.GameProgress
{
    public class EndGame
    {
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _sceneLoader;

        public EndGame(GameData gameData, IAsyncSceneLoading sceneLoader, AudioManager audioManager)
        {
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _audioManager = audioManager;
        }

        public async void End(bool success)
        {
            if(success)
                _gameData.OpenNextLevel();
            await _sceneLoader.UnloadAsync(Scenes.GAME);
            await _sceneLoader.LoadAsync(Scenes.MENU);
            _audioManager.PlayMenuMusic();
        }
    }
}