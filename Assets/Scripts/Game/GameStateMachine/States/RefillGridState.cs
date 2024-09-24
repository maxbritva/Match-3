using System;
using System.Collections.Generic;
using System.Threading;
using Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Grid;
using Game.MatchTiles;
using Game.Tiles;
using UnityEngine;

namespace Game.GameStateMachine.States
{
    public class RefillGridState: IState
    {
        private GridSystem _grid;
        private TilePool _tilePool;
        private MatchFinder _matchFinder;
        private IStateSwitcher _stateSwitcher;
        private CancellationTokenSource _cts;
        private Transform _parent;
        private AudioManager _audioManager;
        private List<Vector2Int> _tilesToRefill = new List<Vector2Int>();
        
        public RefillGridState(GridSystem grid, IStateSwitcher stateSwitcher, MatchFinder matchFinder, 
            TilePool tilePool, Transform parent, AudioManager audioManager)
        {
            _grid = grid;
            _matchFinder = matchFinder;
            _stateSwitcher = stateSwitcher;
            _tilePool = tilePool;
            _parent = parent;
            _audioManager = audioManager;
        }

        public async void Enter()
        {
         
            await FallTiles();
            await RefillGrid();

            for (int i = 0; i < _grid.Width; i++)
            {
                for (int j = 0; j < _grid.Height; j++)
                {
                    if (_grid.GetValue(i, j) == null)
                        Debug.Log(_grid.GridToWorld(i, j));
                }
            }
            if (_matchFinder.CheckBoardForMatches(_grid))
            {
                _stateSwitcher.SwitchState<RemoveTilesState>();
                _audioManager.PlayMatch();
            }
             
            else
            {
                _audioManager.PlayNoMatch();
                _stateSwitcher.SwitchState<PlayerTurnState>();
            }
        }

        public void Exit() => _cts?.Cancel();
        
       
        private async UniTask FallTiles()
        {
            _cts = new CancellationTokenSource();
            for (var x = 0; x < _grid.Width; x++) 
            {
                for (var y = 0; y < _grid.Height; y++)
                {
                    if (_grid.GetValue(x, y)) continue;
                    for (var i = y + 1; i < _grid.Height; i++)
                    {
                        if (_grid.GetValue(x, i) == null) continue;
                        if (_grid.GetValue(x, i).IsInteractable == false) continue;
                        var tile = _grid.GetValue(x, i);
                        _grid.SetValue(x, y, tile);
                        tile.transform.DOLocalMove(_grid.GridToWorld(x, y), 0.2f).SetEase(Ease.InBack); 
                        _grid.SetValue(x, i, null);
                        _tilesToRefill.Add(new Vector2Int(x,i));
                        break;
                    }
                }
            }
            _audioManager.PlayWhoosh();
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), _cts.IsCancellationRequested);
            _cts.Cancel();
        }
        private async UniTask RefillGrid()
        {
            _cts = new CancellationTokenSource();
            for (var x = 0; x < _grid.Width; x++) {
                for (var y = 0; y <_grid.Height; y++)
                {
                    if (_grid.Grid.GetValue(x, y) != null) continue;
                    var tileFromPool = _tilePool.GetTileFromPool(_grid.GridToWorld(x, y), _parent);
                    _grid.SetValue(x, y, tileFromPool);
                    tileFromPool.transform.localScale = Vector3.one * 0.1f;
                    tileFromPool.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutCubic);
                    _audioManager.PlayPop();
                    await UniTask.Delay(TimeSpan.FromSeconds(0.1f), _cts.IsCancellationRequested);
                }
            }
            _cts.Cancel();
        }
    }
}