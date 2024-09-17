using Game.Board;
using Game.Level;
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
        private LevelConfiguration _levelConfiguration;
        private BlankTilesSetup _blankTilesSetup;
        private BackgroundTilesSetup _backgroundTilesSetup;
        
        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard, LevelConfiguration levelConfiguration, BlankTilesSetup blankTilesSetup, BackgroundTilesSetup backgroundTilesSetup)
        {
            _stateSwitcher = stateSwitcher;
            _gameBoard = gameBoard;
            _levelConfiguration = levelConfiguration;
            _blankTilesSetup = blankTilesSetup;
            _backgroundTilesSetup = backgroundTilesSetup;
        }

        public void Enter()
        {
            _setupCamera = new SetupCamera(false);
            _setupCamera.SetCamera(_levelConfiguration.GridWidth, _levelConfiguration.GridHeight);
            _blankTilesSetup.Generate(_levelConfiguration);
            _backgroundTilesSetup.SetupBackground(_gameBoard.transform, _blankTilesSetup.Blanks,_levelConfiguration.GridWidth, _levelConfiguration.GridHeight);
            _gameBoard.CreateBoard();
            _stateSwitcher.SwitchState<PlayerTurnState>();
        }

        public void Exit() => Debug.Log("Game was started");
    }
}