using Game.Board;
using Game.Grid;
using Game.Tiles;
using Game.Utils;
using Level;
using UnityEngine;

namespace Game.GameStateMachine.States
{
    public class PrepareState: IState
    {
        private IStateSwitcher _stateSwitcher;
        private GameBoard _gameBoard;
        private SetupCamera _setupCamera;
        private LevelConfiguration _levelConfiguration;
       

        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard, LevelConfiguration levelConfiguration)
        {
            _stateSwitcher = stateSwitcher;
            _gameBoard = gameBoard;
            _levelConfiguration = levelConfiguration;
        }

        public void Enter()
        {
            _setupCamera = new SetupCamera(false);
            _setupCamera.SetCamera(_levelConfiguration.GridWidth, _levelConfiguration.GridHeight);
            _gameBoard.InitializeBoard();
            _stateSwitcher.SwitchState<PlayerTurnState>();
        }

        public void Exit() => Debug.Log("Game was started");
    }
}