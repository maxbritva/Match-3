using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.GameLoop;
using Game.Grid;
using UnityEngine;

namespace Game.GameStateMachine.States
{
    public class GameLoopState: IState
    {
        private GridSystem _grid;
        private MatchFinder _matchFinder;
        private IStateSwitcher _stateSwitcher;
        private CancellationTokenSource _cts;

        public GameLoopState(GridSystem grid, IStateSwitcher stateSwitcher, MatchFinder matchFinder)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _matchFinder = matchFinder;
        }

        public async void Enter()
        {
            _cts = new CancellationTokenSource();
            await SwapTiles(_grid.CurrentPosition, _grid.TargetPosition);
            if (_matchFinder.CheckBoardForMatches(_grid) == false)
            {
                await SwapTiles(_grid.TargetPosition, _grid.CurrentPosition);
                // hod -
            }
            _stateSwitcher.SwitchState<PlayerTurnState>();
        }

        public void Exit()
        {
            _cts.Cancel();
        }
        
        private async UniTask SwapTiles(Vector2Int current, Vector2Int target)
         {
             var currentTile = _grid.GetValue(current.x, current.y);
             var targetTile =  _grid.GetValue(target.x, target.y);
         
             currentTile.transform.DOLocalMove(_grid.GridToWorld(target.x, target.y), 
                 0.5f).SetEase(Ease.OutCubic).ToUniTask();
         
             targetTile.transform.DOLocalMove(_grid.GridToWorld(current.x, current.y), 
                     0.5f).SetEase(Ease.OutCubic).ToUniTask();
             
             _grid.SetValue(current.x, current.y, targetTile);
             _grid.SetValue(target.x, target.y, currentTile);
         
             await UniTask.Delay(TimeSpan.FromSeconds(0.5f), _cts.IsCancellationRequested);
         }
    }
}