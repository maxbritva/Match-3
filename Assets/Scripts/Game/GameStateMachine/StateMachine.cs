using System.Collections.Generic;
using System.Linq;
using Game.Board;
using Game.GameLoop;
using Game.GameStateMachine.States;
using Game.Grid;
using Level;

namespace Game.GameStateMachine
{
    public class StateMachine: IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        private GameBoard _gameBoard;
        private GridSystem _grid;
        private MatchFinder _matchFinder;
        private LevelConfiguration _levelConfiguration;

        public StateMachine(GameBoard gameBoard, LevelConfiguration levelConfiguration, GridSystem grid, MatchFinder matchFinder)
        {
            _gameBoard = gameBoard;
            _grid = grid;
            _levelConfiguration = levelConfiguration;
            _matchFinder = matchFinder;
            _states = new List<IState>()
            {
                new PrepareState( this,_gameBoard, _levelConfiguration),
                new PlayerTurnState(_grid, this),
                new GameLoopState(_grid, this, _matchFinder)
            };
            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
            var state = _states.FirstOrDefault(state => state is T);
            _currentState.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
    }
}