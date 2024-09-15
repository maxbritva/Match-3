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
        private LevelConfiguration _levelConfiguration;
        private StateMachine _stateMachine;
        private GameProgress _gameProgress;
        private MatchFinder _matchFinder;
        private GridSystem _gridSystem;
        private GameBoard _gameBoard;
        private GameDebug _gameDebug;
        private TilePool _tilePool;
        private ScoreCalculator _scoreCalculator;
       
        private bool _isDebugging;

        public GameEntryPoint(GameBoard gameBoard, GameDebug gameDebug, GridSystem gridSystem, MatchFinder matchFinder, TilePool tilePool, GameProgress gameProgress, ScoreCalculator scoreCalculator)
        {
            _gameBoard = gameBoard;
            _gameDebug = gameDebug;
            _gridSystem = gridSystem;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _scoreCalculator = scoreCalculator;
            _gameProgress = gameProgress;
        }

        public void Initialize()
        {
            _levelConfiguration = Resources.Load<LevelConfiguration>("Levels/1");
            if(_isDebugging)
                _gameDebug.ShowDebugGrid(_levelConfiguration.GridWidth, _levelConfiguration.GridHeight, null);
            _gridSystem.SetupGrid(_levelConfiguration.GridWidth, _levelConfiguration.GridHeight);
            _stateMachine = new StateMachine(_gameBoard, _levelConfiguration, _gridSystem, _matchFinder, _tilePool, _gameProgress, _scoreCalculator);
            _gameProgress.LoadLevelConfiguration(_levelConfiguration.GoalScore, _levelConfiguration.Moves);
        }
    }
}