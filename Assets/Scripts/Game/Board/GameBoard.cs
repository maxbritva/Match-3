using System.Collections.Generic;
using Game.GameLoop;
using Game.Grid;
using Game.Tiles;
using Level;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using GridSystem = Game.Grid.GridSystem;

namespace Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private LevelConfiguration _levelConfiguration;
        // public int GridWidth { get; private set; }
        // public int GridHeight { get; private set; }
        private List<Tile> _tilesToRefill = new List<Tile>();
        private GridSystem _grid;
        private TilePool _tilePool;
        private BlankTilesLevelSetup _blankTilesLevelSetup;
        private MatchFinder _matchFinder;
        
        private bool _isDebugging;

        public void InitializeBoard()
        {
            // GridWidth = _grid.Width;
            // GridHeight = _grid.Height;
            _blankTilesLevelSetup.Generate(_levelConfiguration);
         
            FillBoard();
            while (_matchFinder.CheckBoardForMatches(_grid)) 
                FillBoard();
        }

        private void FillBoard()
        {
            ClearBoard();
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_blankTilesLevelSetup.Blanks[x, y])
                    {
                        if (_grid.GetValue(x, y)) continue;
                        var tile = _tilePool.CreateBlankTile(_grid.GridToWorld(x, y), transform);
                        _grid.SetValue(x, y, tile);
                    }
                    else
                    {
                        var tile = _tilePool.GetTileFromPool(_grid.GridToWorld(x, y), transform);
                        _grid.SetValue(x, y, tile);
                        _tilesToRefill.Add(tile);
                    }
                }
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

       [Inject] private void Construct(GridSystem gridSystem, TilePool tilePool, BlankTilesLevelSetup blankTilesLevelSetup, MatchFinder matchFinder)
       {
            _grid = gridSystem;
            _blankTilesLevelSetup = blankTilesLevelSetup;
            _tilePool = tilePool;
            _matchFinder = matchFinder;
        }
    }
}