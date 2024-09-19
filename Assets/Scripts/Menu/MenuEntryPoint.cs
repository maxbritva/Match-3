using Audio;
using Data;
using Menu.Levels;
using Menu.UI;
using SceneLoading;
using VContainer.Unity;

namespace Menu
{
    public class MenuEntryPoint: IInitializable
    {
        private GameData _gameData;
        private SceneLoader _sceneLoader;
        private AudioManager _audioManager;
        private SetupLevelSequence _setupLevelSequence;
        private MenuLevelsSequenceView _levelsSequenceView;
        private MenuAnimator _menuAnimator;
       
        public MenuEntryPoint(GameData gameData, SceneLoader sceneLoader, AudioManager audioManager, SetupLevelSequence setupLevelSequence, 
            MenuLevelsSequenceView menuLevelsSequenceView, MenuAnimator menuAnimator)
        {
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _audioManager = audioManager;
            _setupLevelSequence = setupLevelSequence;
            _levelsSequenceView = menuLevelsSequenceView;
            _menuAnimator = menuAnimator;
        }

        public async void Initialize()
        {
            await _setupLevelSequence.Setup(_gameData.CurrentLevel);
            _levelsSequenceView.SetupButtonsView();
            _audioManager.SetSoundVolume();
            await _menuAnimator.StartAnimation();
        }

     
    }
}