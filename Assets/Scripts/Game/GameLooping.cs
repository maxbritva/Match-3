using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Grid;
using Game.Tiles;
using Unity.VisualScripting;
using UnityEngine;


namespace Game
{
    public class GameLooping
    {
        private Board.GameBoard _gameBoard;
        private GridCoordinator _gridCoordinator;
        private CancellationTokenSource _cancellationToken;
        private TileCreator _tileCreator;

        public GameLooping(Board.GameBoard gameBoard, GridCoordinator gridCoordinator, TileCreator tileCreator)
        {
            _gameBoard = gameBoard;
            _gridCoordinator = gridCoordinator;
            _tileCreator = tileCreator;
        }

       //  public async UniTask RunGameLoop(Vector2Int gridPosA, Vector2Int gridPosB)
       //  {
       //      _cancellationToken = new CancellationTokenSource();
       //      await SwapTiles(gridPosA, gridPosB);
       //      _cancellationToken.Cancel();
       //      Debug.Log("Completed");
       //      List<Vector2Int> matches = FindMatches();
       //      await RemoveTiles(matches);
       //      await FallTiles();
       //      await FillGrid();
       //  }
       //
       //  // private async UniTask SwapTiles(Vector2Int gridPosA, Vector2Int gridPosB)
       //  // {
       //  //     var gridObjectA = _board.Grid.GetValue(gridPosA.x, gridPosA.y);
       //  //     var gridObjectB =  _board.Grid.GetValue(gridPosB.x, gridPosB.y);
       //  //
       //  //      gridObjectA.transform.DOLocalMove(_gridCoordinator.GridToWorld(gridPosB.x, gridPosB.y,
       //  //         _board.CellSize, _board.OriginPosition), 0.5f).SetEase(Ease.OutCubic).ToUniTask();
       //  //
       //  //      gridObjectB.transform.DOLocalMove(_gridCoordinator.GridToWorld(gridPosA.x, gridPosA.y,
       //  //             _board.CellSize, _board.OriginPosition), 0.5f)
       //  //         .SetEase(Ease.OutCubic).ToUniTask();
       //  //     
       //  //     _board.Grid.SetValue(gridPosA.x, gridPosA.y, gridObjectB);
       //  //     _board.Grid.SetValue(gridPosB.x, gridPosB.y, gridObjectA);
       //  //
       //  //     await UniTask.Delay(TimeSpan.FromSeconds(0.5f), _cancellationToken.IsCancellationRequested);
       //  // }
       //  
       // private List<Vector2Int> FindMatches() 
       // {
       //     
       //      HashSet<Vector2Int> matches = new HashSet<Vector2Int>();
       //      
       //      // Horizontal
       //      for (var y = 0; y < _board.GridHeight; y++) {
       //          for (var x = 0; x < _board.GridWidth - 2; x++) {
       //              var gemA = _board.Grid.GetValue(x, y);
       //              var gemB = _board.Grid.GetValue(x + 1, y);
       //              var gemC = _board.Grid.GetValue(x + 2, y);
       //              
       //              if (gemA == null || gemB == null || gemC == null) continue;
       //              
       //              if (gemA.GetTileType() == gemB.GetTileType()
       //                  && gemB.GetTileType() == gemC.GetTileType()) 
       //              {
       //                  matches.Add(new Vector2Int(x, y));
       //                  matches.Add(new Vector2Int(x + 1, y));
       //                  matches.Add(new Vector2Int(x + 2, y));
       //              }
       //          }
       //      }
       //      
       //      // Vertical
       //      for (var x = 0; x < _board.GridWidth; x++) {
       //          for (var y = 0; y < _board.GridHeight - 2; y++) {
       //              var gemA = _board.Grid.GetValue(x, y);
       //              var gemB = _board.Grid.GetValue(x, y + 1);
       //              var gemC = _board.Grid.GetValue(x, y + 2);
       //              
       //              if (gemA == null || gemB == null || gemC == null) continue;
       //              
       //              if (gemA.GetTileType() == gemB.GetTileType()
       //                  && gemB.GetTileType() == gemC.GetTileType())
       //              {
       //                  matches.Add(new Vector2Int(x, y));
       //                  matches.Add(new Vector2Int(x, y + 1));
       //                  matches.Add(new Vector2Int(x, y + 2));
       //              }
       //          }
       //      }
       //
       //      if (matches.Count == 0) {
       //        //  audioManager.PlayNoMatch();
       //      } else {
       //        //  audioManager.PlayMatch();
       //      }
       //      return new List<Vector2Int>(matches);
       //  }
       //
       //
       // private async UniTask RemoveTiles(List<Vector2Int> matches)
       // {
       //    // audioManager.PlayPop();
       //    foreach (var match in matches)
       //    {
       //        var gem = _board.Grid.GetValue(match.x, match.y);
       //        _board.Grid.SetValue(match.x, match.y, null);
       //
       //      //  ExplodeVFX(match);
       //          
       //        await gem.transform.DOPunchScale(Vector3.one * 0.1f, 0.1f, 1, 0.5f);
       //       // gem.GameObject().SetActive(false);
       //       _board.Destroy(gem.GameObject());
       //    }
       // }
       //
       // private async UniTask FallTiles()
       // {
       //     for (var x = 0; x < _board.GridWidth; x++) {
       //         for (var y = 0; y < _board.GridHeight; y++) {
       //             if (_board.Grid.GetValue(x, y) == null) {
       //                 for (var i = y + 1; i < _board.GridHeight; i++) {
       //                     if (_board.Grid.GetValue(x, i) != null) {
       //                         var gem = _board.Grid.GetValue(x, i);
       //                         _board.Grid.SetValue(x, y, _board.Grid.GetValue(x, i));
       //                         _board.Grid.SetValue(x, i, null);
       //                         gem.transform.DOLocalMove(_gridCoordinator.GridToWorld(x,y, _board.CellSize, _board.OriginPosition), 0.5f).SetEase(Ease.InBack);
       //                        // audioManager.PlayWoosh();
       //                        break;
       //                     }
       //                 }
       //             }
       //         }
       //     }
       // }
       //
       // private async UniTask FillGrid()
       // {
       //     for (var x = 0; x < _board.GridWidth; x++) {
       //         for (var y = 0; y < _board.GridHeight; y++) {
       //             if (_board.Grid.GetValue(x, y) == null)
       //             {
       //                 _tileCreator.CreateTile(_gridCoordinator.GridToWorld(x, y, _board.CellSize, _board.OriginPosition), _board.transform);
       //               //  audioManager.PlayPop();
       //                await  UniTask.Delay(TimeSpan.FromSeconds(0.1f), _cancellationToken.IsCancellationRequested);
       //             }
       //         }
       //     }
       // }
  
    }
}