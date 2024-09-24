using System;
using System.Collections.Generic;
using System.Threading;
using Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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

        public RemoveTilesState(GridSystem grid, MatchFinder matchFinder, IStateSwitcher stateSwitcher, 
            ScoreCalculator scoreCalculator, AudioManager audioManager)
        {
            _grid = grid;
            _matchFinder = matchFinder;
            _audioManager = audioManager;
            _stateSwitcher = stateSwitcher;
            _scoreCalculator = scoreCalculator;
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
                _audioManager.PlayPop();
                Vector2Int position = grid.WorldToGrid(tile.transform.position);
                grid.SetValue(position.x, position.y, null);
                tile.transform.DOPunchScale(Vector3.one * 0.1f, 3f, 1, 0.5f).SetEase(Ease.OutBounce);
              //  await UniTask.Delay(TimeSpan.FromSeconds(0.05f), _cts.IsCancellationRequested);
                tile.GameObject().SetActive(false);
            }
            _cts.Cancel();
        }
    }
}