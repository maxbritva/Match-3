using System.Collections.Generic;
using System.Threading;
using Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        private GridSystem _grid;
        private MatchFinder _matchFinder;
        private ScoreCalculator _scoreCalculator;
        private IStateSwitcher _stateSwitcher;
        private CancellationTokenSource _cts;
        private AudioManager _audioManager;
        private Transform _parent;
        private FXPool _fxPool;

        public RemoveTilesState(GridSystem grid, MatchFinder matchFinder, IStateSwitcher stateSwitcher, 
            ScoreCalculator scoreCalculator, AudioManager audioManager, FXPool fxPool, Transform parent)
        {
            _grid = grid;
            _fxPool = fxPool;
            _matchFinder = matchFinder;
            _audioManager = audioManager;
            _stateSwitcher = stateSwitcher;
            _scoreCalculator = scoreCalculator;
            _parent = parent;
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
                await tile.transform.DOScale(Vector3.zero, 0.05f).SetEase(Ease.OutBounce);
                _fxPool.GetFXFromPool(tile.GameObject().transform.position, _parent);
                tile.GameObject().SetActive(false);
                tile.transform.localScale = Vector3.one;
            }
            _cts.Cancel();
        }
    }
}