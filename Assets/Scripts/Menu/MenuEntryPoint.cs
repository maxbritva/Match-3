using Audio;
using Data;
using Level;
using Menu.Levels;
using Menu.UI;
using SceneLoading;
using VContainer.Unity;

namespace Menu
{
    public class MenuEntryPoint: IInitializable
    {
        private GameData _gameData;
        private AudioManager _audioManager;
        private SetupLevelSequence _setupLevelSequence;
        private LevelsSequenceView _levelsSequenceView;
        private MenuAnimator _menuAnimator;
        private IAsyncSceneLoading _sceneLoader;
       
        public MenuEntryPoint(GameData gameData, AudioManager audioManager, SetupLevelSequence setupLevelSequence, 
            LevelsSequenceView levelsSequenceView, MenuAnimator menuAnimator, IAsyncSceneLoading sceneLoader)
        {
            _gameData = gameData;
            _audioManager = audioManager;
            _setupLevelSequence = setupLevelSequence;
            _levelsSequenceView = levelsSequenceView;
            _menuAnimator = menuAnimator;
            _sceneLoader = sceneLoader;
        }

        public async void Initialize()
        {
            await _setupLevelSequence.Setup(_gameData.CurrentLevelIndex);
            _levelsSequenceView.SetupButtonsView(_gameData.CurrentLevelIndex);
            _audioManager.PlayMenuMusic();
            await _menuAnimator.StartAnimation();
        }
    }
}