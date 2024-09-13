using Game.Board;
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
        private GridSystem _gridSystem;
        private bool _isDebugging;

        public GameEntryPoint(GameBoard gameBoard, GameDebug gameDebug, GridSystem gridSystem)
        {
            _gameBoard = gameBoard;
            _gameDebug = gameDebug;
            _gridSystem = gridSystem;
        }

        public void Initialize()
        {
            _levelConfiguration = Resources.Load<LevelConfiguration>("Levels/1");
            if(_isDebugging)
                _gameDebug.ShowDebugGrid(_levelConfiguration.LevelGridWidth, _levelConfiguration.LevelGridHeight, null);
            _gridSystem.SetupGrid(_levelConfiguration.LevelGridWidth, _levelConfiguration.LevelGridHeight);
            _stateMachine = new StateMachine(_gameBoard, _levelConfiguration);
        }
    }
}