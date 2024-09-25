using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.MatchTiles;
using Game.Tiles;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using GridSystem = Game.Grid.GridSystem;

namespace Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        private List<Tile> _tilesToRefill = new List<Tile>();
        
        private BlankTilesSetup _blankTilesSetup;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GridSystem _grid;
        
        public void CreateBoard()
        {
            FillBoard();
            while (_matchFinder.CheckBoardForMatches(_grid)) 
                FillBoard();
         
            _matchFinder.ClearTilesToRemoveList();
        }

        private async void FillBoard()
        {
            ClearBoard();
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_blankTilesSetup.Blanks[x, y])
                    {
                        if (_grid.GetValue(x, y)) continue;
                        var tile = _tilePool.CreateBlankTile(_grid.GridToWorld(x, y), transform);
                        _grid.SetValue(x, y, tile);
                    }
                    else
                    {
                        var tile = _tilePool.GetTileFromPool(_grid.GridToWorld(x, y), transform);
                        _grid.SetValue(x, y, tile);
                        tile.GameObject().SetActive(true);
                       await AnimateTile(tile.GameObject(), new CancellationToken());
                        _tilesToRefill.Add(tile);
                    }
                }
            }
        }
        private async UniTask AnimateTile(GameObject target, CancellationToken cancellationToken)
        {
            target.transform.localScale = Vector3.one * 0.1f;
            target.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
            UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken.IsCancellationRequested);
        }

        private void ClearBoard()
        {
            if (_tilesToRefill == null) return;
            foreach (var potion in _tilesToRefill)
            {
                _grid.SetValue(potion.GameObject().transform.position, null);
                potion.GameObject().SetActive(false);
            }
            _tilesToRefill.Clear();
        }
        
       [Inject] private void Construct(GridSystem gridSystem, TilePool tilePool, BlankTilesSetup blankTilesSetup, MatchFinder matchFinder)
       {
            _grid = gridSystem;
            _blankTilesSetup = blankTilesSetup;
            _tilePool = tilePool;
            _matchFinder = matchFinder;
       }
    }
}