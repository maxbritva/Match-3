using Game.Board;
using Game.GameStateMachine;
using Game.Grid;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Level;
using UnityEngine;
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
        private readonly GameProgress _gameProgress;
        private readonly MatchFinder _matchFinder;
        private readonly GridSystem _gridSystem;
        private readonly GameBoard _gameBoard;
        private readonly GameDebug _gameDebug;
        private readonly TilePool _tilePool;

        private bool _isDebugging;

        public GameEntryPoint(GameBoard gameBoard, GameDebug gameDebug, GridSystem gridSystem, MatchFinder matchFinder, TilePool tilePool, 
            GameProgress gameProgress, ScoreCalculator scoreCalculator,  BackgroundTilesSetup backgroundTilesSetup, BlankTilesSetup blankTilesSetup)
        {
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
            _levelConfiguration = Resources.Load<LevelConfiguration>("Levels/1");
            if(_isDebugging)
                _gameDebug.ShowDebugGrid(_levelConfiguration.GridWidth, _levelConfiguration.GridHeight, null);
            _gridSystem.SetupGrid(_levelConfiguration.GridWidth, _levelConfiguration.GridHeight);
            _stateMachine = new StateMachine(_gameBoard, _levelConfiguration, _gridSystem, 
                _matchFinder, _tilePool, _gameProgress, _scoreCalculator, _backgroundTilesSetup, _blankTilesSetup);
            _gameProgress.LoadLevelConfiguration(_levelConfiguration.GoalScore, _levelConfiguration.Moves);
        }
    }
}