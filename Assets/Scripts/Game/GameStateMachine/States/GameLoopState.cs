using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.GameLoop;
using Game.Grid;
using Game.Tiles;
using Unity.VisualScripting;
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
            else
            {
                RemoveTiles(_matchFinder.TilesToRemove, _grid);
              //  _matchFinder.ClearTilesToRemoveList();
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

        private async void RemoveTiles(List<Tile> tilesToRemove, GridSystem grid)
        {
            foreach (var tile in tilesToRemove)
            {
               // audioManager.PlayPop();
               Vector2Int position = grid.WorldToGrid(tile.transform.position);
               grid.SetValue(position.x, position.y, null);
               await tile.transform.DOPunchScale(Vector3.one * 0.1f, 0.1f, 1, 0.5f);
                tile.GameObject().SetActive(false);
            }
        }
    }
}