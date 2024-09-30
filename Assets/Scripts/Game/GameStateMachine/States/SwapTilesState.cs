using System;
using System.Threading;
using Animations;
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
        private readonly AudioManager _audioManager;
        private readonly GameProgress.GameProgress _gameProgress;
        private readonly MatchFinder _matchFinder;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IAnimation _animation;
        private CancellationTokenSource _cts;

        public SwapTilesState(GridSystem grid, IStateSwitcher stateSwitcher, MatchFinder matchFinder, 
            GameProgress.GameProgress gameProgress, AudioManager audioManager, IAnimation animation)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _matchFinder = matchFinder;
            _gameProgress = gameProgress;
            _audioManager = audioManager;
            _animation = animation;
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
            _animation.MoveTile(tileToMove, _grid.GridToWorld(position.x, position.y), Ease.OutCubic);

    }
}