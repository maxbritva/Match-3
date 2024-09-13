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
        private bool _isDebugging;

        public GameEntryPoint(GameBoard gameBoard, GameDebug gameDebug)
        {
            _gameBoard = gameBoard;
            _gameDebug = gameDebug;
        }

        public void Initialize()
        {
         
            _levelConfiguration = Resources.Load<LevelConfiguration>("Levels/1");
            if(_isDebugging)
                _gameDebug.ShowDebugGrid(_levelConfiguration.LevelGridWidth, _levelConfiguration.LevelGridHeight, null);
            
            _stateMachine = new StateMachine(_gameBoard);
        }
    }
}