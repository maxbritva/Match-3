using System.Collections.Generic;
using System.Threading;
using Animations;
using Audio;
using Cysharp.Threading.Tasks;
using Game.FX;
using Game.Grid;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.GameStateMachine.States
{
    public class RemoveTilesState: IState
    {
        private readonly GridSystem _grid;
        private readonly MatchFinder _matchFinder;
        private readonly ScoreCalculator _scoreCalculator;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly AudioManager _audioManager;
        private readonly Transform _parent;
        private readonly FXPool _fxPool;
        private readonly IAnimation _animation;
        private CancellationTokenSource _cts;

        public RemoveTilesState(GridSystem grid, MatchFinder matchFinder, IStateSwitcher stateSwitcher, 
            ScoreCalculator scoreCalculator, AudioManager audioManager, FXPool fxPool, Transform parent, IAnimation animation)
        {
            _grid = grid;
            _fxPool = fxPool;
            _matchFinder = matchFinder;
            _audioManager = audioManager;
            _stateSwitcher = stateSwitcher;
            _scoreCalculator = scoreCalculator;
            _parent = parent;
            _animation = animation;
        }

        public async void Enter()
        {
            _cts = new CancellationTokenSource();
            _scoreCalculator.CalculateScoreToAdd(_matchFinder.CurrentMatchResult.MatchDirection);
            await RemoveTiles(_matchFinder.TilesToRemove, _grid);
            _stateSwitcher.SwitchState<RefillGridState>();
        }

        public void Exit()
        {
            _matchFinder.ClearTilesToRemoveList();
            _cts?.Cancel();
        }

        private async UniTask RemoveTiles(List<Tile> tilesToRemove, GridSystem grid)
        {
            foreach (var tile in tilesToRemove)
            {
                _audioManager.PlayRemove();
                Vector2Int position = grid.WorldToGrid(tile.transform.position);
                grid.SetValue(position.x, position.y, null);
                await _animation.HideTile(tile.GameObject());
                _fxPool.GetFXFromPool(tile.GameObject().transform.position, _parent);
            }
            _cts.Cancel();
        }
    }
}