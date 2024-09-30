using System;
using System.Collections.Generic;
using System.Threading;
using Animations;
using Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Grid;
using Game.MatchTiles;
using Game.Tiles;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.GameStateMachine.States
{
    public class RefillGridState: IState
    {
        private readonly GridSystem _grid;
        private readonly TilePool _tilePool;
        private readonly GameProgress.GameProgress _gameProgress;
        private readonly MatchFinder _matchFinder;
        private readonly IStateSwitcher _stateSwitcher;
        private CancellationTokenSource _cts;
        private readonly Transform _parent;
        private readonly AudioManager _audioManager;
        private IAnimation _animation;
        private readonly List<Vector2Int> _tilesToRefill = new List<Vector2Int>();
        
        public RefillGridState(GridSystem grid, IStateSwitcher stateSwitcher, MatchFinder matchFinder, TilePool tilePool, 
            Transform parent, AudioManager audioManager, GameProgress.GameProgress gameProgress, IAnimation animation)
        {
            _grid = grid;
            _matchFinder = matchFinder;
            _stateSwitcher = stateSwitcher;
            _tilePool = tilePool;
            _parent = parent;
            _animation = animation;
            _audioManager = audioManager;
            _gameProgress = gameProgress;
        }

        public async void Enter()
        {
            await FallTiles();
            await RefillGrid();
            if (_matchFinder.CheckBoardForMatches(_grid))
            {
                _stateSwitcher.SwitchState<RemoveTilesState>();
                _audioManager.PlayMatch();
            }
            else
            {
                _audioManager.PlayNoMatch();
                CheckEndGame();
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
                        _animation.MoveTile(tile, _grid.GridToWorld(x, y), Ease.InBack);
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
                    tileFromPool.GameObject().SetActive(true);
                    _grid.SetValue(x, y, tileFromPool);
                    _animation.Reveal(tileFromPool.GameObject(), 0.2f);
                    _audioManager.PlayPop();
                    await UniTask.Delay(TimeSpan.FromSeconds(0.1f), _cts.IsCancellationRequested);
                }
            }
            _cts.Cancel();
        }

        private void CheckEndGame()
        {
            if (_gameProgress.CheckGoalScore())
                _stateSwitcher.SwitchState<WinState>();
            else if (_gameProgress.Moves <= 0)
                _stateSwitcher.SwitchState<LooseState>();
            else
                _stateSwitcher.SwitchState<PlayerTurnState>();
        }
        
    }
}