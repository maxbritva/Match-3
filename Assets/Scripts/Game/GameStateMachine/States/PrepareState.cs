using Animations;
using Game.Board;
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
        private IAnimation _animationManager;
        private SetupCamera _setupCamera;
        private LevelConfiguration _levelConfiguration;
        private BlankTilesSetup _blankTilesSetup;
        private BackgroundTilesSetup _backgroundTilesSetup;
        
        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard, LevelConfiguration levelConfiguration, 
            BlankTilesSetup blankTilesSetup, BackgroundTilesSetup backgroundTilesSetup, IAnimation animationManager)
        {
            _stateSwitcher = stateSwitcher;
            _gameBoard = gameBoard;
            _levelConfiguration = levelConfiguration;
            _blankTilesSetup = blankTilesSetup;
            _backgroundTilesSetup = backgroundTilesSetup;
            _animationManager = animationManager;
        }

        public async void Enter()
        {
           await _backgroundTilesSetup.SetupBackground(_gameBoard.transform, _blankTilesSetup.Blanks,
                _levelConfiguration.GridWidth, _levelConfiguration.GridHeight, _animationManager);
            _gameBoard.CreateBoard();
            _stateSwitcher.SwitchState<PlayerTurnState>();
        }

        public void Exit() => Debug.Log("Game was started");
    }
}