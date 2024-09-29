using System.Collections.Generic;
using Animations;
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
        private readonly List<Tile> _tilesToRefill = new List<Tile>();
        
        private BlankTilesSetup _blankTilesSetup;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GridSystem _grid;
        private IAnimation _animationManager;
        
        public void CreateBoard()
        {
            FillBoard();
            while (_matchFinder.CheckBoardForMatches(_grid)) 
                FillBoard();
         
            _matchFinder.ClearTilesToRemoveList();
            RevealTiles();
        }

        private void FillBoard()
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
                        _tilesToRefill.Add(tile);
                    }
                }
            }
        }
        private void RevealTiles()
        {
            foreach (var tile in _tilesToRefill)
            {
                var target = tile.GameObject();
                _animationManager.Reveal(target, 1f);
            }
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
        
       [Inject] private void Construct(GridSystem gridSystem, TilePool tilePool, BlankTilesSetup blankTilesSetup, MatchFinder matchFinder, IAnimation animationManager)
       {
            _grid = gridSystem;
            _blankTilesSetup = blankTilesSetup;
            _tilePool = tilePool;
            _matchFinder = matchFinder;
            _animationManager = animationManager;
       }
    }
}