using System.Collections.Generic;
using System.Linq;
using Game.Board;
using Game.GameStateMachine.States;
using Game.Grid;
using Game.Level;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;

namespace Game.GameStateMachine
{
    public class StateMachine: IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        private GameBoard _gameBoard;
        private TilePool _tilePool;
        private GridSystem _grid;
        private MatchFinder _matchFinder;
        private GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;
        private LevelConfiguration _levelConfiguration;
        private BlankTilesSetup _blankTilesSetup;
        private BackgroundTilesSetup _backgroundTilesSetup;

        public StateMachine(GameBoard gameBoard, LevelConfiguration levelConfiguration, GridSystem grid, MatchFinder matchFinder, TilePool tilePool, GameProgress gameProgress, 
            ScoreCalculator scoreCalculator, BackgroundTilesSetup backgroundTilesSetup, BlankTilesSetup blankTilesSetup)
        {
            _gameBoard = gameBoard;
            _grid = grid;
            _tilePool = tilePool;
            _levelConfiguration = levelConfiguration;
            _matchFinder = matchFinder;
            _gameProgress = gameProgress;
            _scoreCalculator = scoreCalculator;
            _blankTilesSetup = blankTilesSetup;
            _backgroundTilesSetup = backgroundTilesSetup;
            _states = new List<IState>()
            {
                new PrepareState( this,_gameBoard, _levelConfiguration, _blankTilesSetup, _backgroundTilesSetup),
                new PlayerTurnState(_grid, this),
                new RemoveTilesState(_grid, _matchFinder,this, _scoreCalculator),
                new SwapTilesState(_grid, this, _matchFinder, _gameProgress),
                new RefillGridState(_grid, this, _matchFinder, _tilePool, _gameBoard.transform)
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