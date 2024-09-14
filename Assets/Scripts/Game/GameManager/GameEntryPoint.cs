using Game.Board;
using Game.GameLoop;
using Game.GameStateMachine;
using Game.Grid;
using Level;
using UnityEngine;
using VContainer.Unity;

namespace Game.GameManager
{
    public class GameEntryPoint: IInitializable
    {
        private StateMachine _stateMachine;
        private GameBoard _gameBoard;
        private LevelConfiguration _levelConfiguration;
        private GameDebug _gameDebug;
        private MatchFinder _matchFinder;
        private GridSystem _gridSystem;
        private bool _isDebugging;

        public GameEntryPoint(GameBoard gameBoard, GameDebug gameDebug, GridSystem gridSystem, MatchFinder matchFinder)
        {
            _gameBoard = gameBoard;
            _gameDebug = gameDebug;
            _gridSystem = gridSystem;
            _matchFinder = matchFinder;
        }

        public void Initialize()
        {
            _levelConfiguration = Resources.Load<LevelConfiguration>("Levels/1");
            if(_isDebugging)
                _gameDebug.ShowDebugGrid(_levelConfiguration.LevelGridWidth, _levelConfiguration.LevelGridHeight, null);
            _gridSystem.SetupGrid(_levelConfiguration.LevelGridWidth, _levelConfiguration.LevelGridHeight);
            _stateMachine = new StateMachine(_gameBoard, _levelConfiguration, _gridSystem, _matchFinder);
        }
    }
}