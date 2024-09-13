using Game.Board;
using Game.Tiles;
using Game.Utils;
using UnityEngine;

namespace Game.GameStateMachine.States
{
    public class PrepareState: IState
    {
        private IStateSwitcher _stateSwitcher;
        private GameBoard _gameBoard;
        private SetupCamera _setupCamera;

        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard)
        {
            _stateSwitcher = stateSwitcher;
            _gameBoard = gameBoard;
        }

        public void Enter()
        {
            _gameBoard.InitializeBoard();
            _setupCamera = new SetupCamera(false);
            _setupCamera.SetCamera(_gameBoard.GridWidth, _gameBoard.GridHeight);
            _stateSwitcher.SwitchState<PlayerTurnState>();
        }

        public void Exit() => Debug.Log("Game was started");
    }
}