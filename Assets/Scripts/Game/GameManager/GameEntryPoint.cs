using Data;
using Game.Board;
using Game.GameStateMachine;
using Game.Grid;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.Utils;
using Level;
using VContainer.Unity;

namespace Game.GameManager
{
    public class GameEntryPoint: IInitializable
    {
        private readonly BackgroundTilesSetup _backgroundTilesSetup;
        private LevelConfiguration _levelConfiguration;
        private readonly ScoreCalculator _scoreCalculator;
        private readonly BlankTilesSetup _blankTilesSetup;
        private StateMachine _stateMachine;
        private readonly GameProgress.GameProgress _gameProgress;
        private readonly MatchFinder _matchFinder;
        private readonly GridSystem _gridSystem;
        private readonly GameBoard _gameBoard;
        private readonly GameDebug _gameDebug;
        private readonly TilePool _tilePool;
        private GameData _gameData;
        private TilesLoader _tilesLoader;
        private SetupCamera _setupCamera;

        private bool _isDebugging;

        public GameEntryPoint(TilesLoader tilesLoader, GameData gameData, GameBoard gameBoard, 
            GameDebug gameDebug, GridSystem gridSystem, MatchFinder matchFinder, TilePool tilePool, 
            GameProgress.GameProgress gameProgress, ScoreCalculator scoreCalculator,  
            BackgroundTilesSetup backgroundTilesSetup, BlankTilesSetup blankTilesSetup)
        {
            _tilesLoader = tilesLoader;
            _gameData = gameData;
            _gameBoard = gameBoard;
            _gameDebug = gameDebug;
            _gridSystem = gridSystem;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _scoreCalculator = scoreCalculator;
            _gameProgress = gameProgress;
            _blankTilesSetup = blankTilesSetup;
            _backgroundTilesSetup = backgroundTilesSetup;
        }

        public void Initialize()
        {
            _levelConfiguration = _gameData.CurrentLevel;
            if(_isDebugging)
                _gameDebug.ShowDebugGrid(_levelConfiguration.GridWidth, _levelConfiguration.GridHeight, null);
            _gridSystem.SetupGrid(_levelConfiguration.GridWidth, _levelConfiguration.GridHeight);
            _stateMachine = new StateMachine(_gameBoard, _levelConfiguration, _gridSystem, 
                _matchFinder, _tilePool, _gameProgress, _scoreCalculator, _backgroundTilesSetup, _blankTilesSetup);
            _gameProgress.LoadLevelConfiguration(_levelConfiguration.GoalScore, _levelConfiguration.Moves);
            _tilesLoader.Load();
            _setupCamera = new SetupCamera(false);
            _setupCamera.SetCamera(_levelConfiguration.GridWidth, _levelConfiguration.GridHeight);
            _blankTilesSetup.Generate(_levelConfiguration);
        }
    }
}