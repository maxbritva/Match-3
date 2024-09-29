﻿using System;
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
    public class SwapTilesState: IState
    {
        private readonly GridSystem _grid;
        private AudioManager _audioManager;
        private readonly GameProgress.GameProgress _gameProgress;
        private readonly MatchFinder _matchFinder;
        private readonly IStateSwitcher _stateSwitcher;
        private CancellationTokenSource _cts;

        public SwapTilesState(GridSystem grid, IStateSwitcher stateSwitcher, MatchFinder matchFinder, 
            GameProgress.GameProgress gameProgress, AudioManager audioManager)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _matchFinder = matchFinder;
            _gameProgress = gameProgress;
            _audioManager = audioManager;
        }

        public async void Enter()
        {
            _cts = new CancellationTokenSource();
            _audioManager.PlayWhoosh();
            await SwapTiles(_grid.CurrentPosition, _grid.TargetPosition);
            if (_matchFinder.CheckBoardForMatches(_grid) == false)
            {
                _audioManager.PlayNoMatch();
                _audioManager.PlayWhoosh();
                await SwapTiles(_grid.TargetPosition, _grid.CurrentPosition);
                _stateSwitcher.SwitchState<PlayerTurnState>();
            }
            else
            {
                _audioManager.PlayMatch();
                _gameProgress.SpendMove();
                _stateSwitcher.SwitchState<RemoveTilesState>();
            }
        }

        public void Exit() => _cts?.Cancel();

        private async UniTask SwapTiles(Vector2Int current, Vector2Int target)
         {
             var currentTile = _grid.GetValue(current.x, current.y);
             var targetTile =  _grid.GetValue(target.x, target.y);
             
             MoveAnimation(currentTile, target);
             MoveAnimation(targetTile, current);
             
             _grid.SetValue(current.x, current.y, targetTile);
             _grid.SetValue(target.x, target.y, currentTile);
         
             await UniTask.Delay(TimeSpan.FromSeconds(0.5f), _cts.IsCancellationRequested);
         }
        private void MoveAnimation(Tile tileToMove, Vector2Int position) =>
            tileToMove.transform.DOLocalMove(_grid.GridToWorld(position.x, position.y),0.4f).SetEase(Ease.OutCubic);
    }
}