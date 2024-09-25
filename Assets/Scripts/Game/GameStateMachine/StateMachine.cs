using System.Collections.Generic;
using System.Linq;
using Animations;
using Audio;
using Game.Board;
using Game.GameStateMachine.States;
using Game.Grid;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Level;

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
        private GameProgress.GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;
        private LevelConfiguration _levelConfiguration;
        private BlankTilesSetup _blankTilesSetup;
        private BackgroundTilesSetup _backgroundTilesSetup;
        private AudioManager _audioManager;
        private IAnimation _animation;

        public StateMachine(GameBoard gameBoard, LevelConfiguration levelConfiguration, GridSystem grid, MatchFinder matchFinder, TilePool tilePool, GameProgress.GameProgress gameProgress, 
            ScoreCalculator scoreCalculator, BackgroundTilesSetup backgroundTilesSetup, BlankTilesSetup blankTilesSetup, AudioManager audioManager, IAnimation animation)
        {
            _gameBoard = gameBoard;
            _grid = grid;
            _tilePool = tilePool;
            _levelConfiguration = levelConfiguration;
            _matchFinder = matchFinder;
            _gameProgress = gameProgress;
            _audioManager = audioManager;
            _scoreCalculator = scoreCalculator;
            _blankTilesSetup = blankTilesSetup;
            _backgroundTilesSetup = backgroundTilesSetup;
            _animation = animation;
            _states = new List<IState>()
            {
                new PrepareState( this,_gameBoard, _levelConfiguration, _blankTilesSetup, _backgroundTilesSetup, _animation),
                new PlayerTurnState(_grid, this, _audioManager),
                new RemoveTilesState(_grid, _matchFinder,this, _scoreCalculator, _audioManager),
                new SwapTilesState(_grid, this, _matchFinder, _gameProgress, _audioManager),
                new RefillGridState(_grid, this, _matchFinder, _tilePool, _gameBoard.transform, _audioManager)
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