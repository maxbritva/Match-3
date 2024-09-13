using System.Collections.Generic;
using Game.GameLoop;
using Game.Grid;
using Game.Tiles;
using Game.Utils;
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
        public GridSystem Grid => _grid;
        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        private List<Tile> _tilesToRefill = new List<Tile>();
        private GridSystem _grid;
        private GridCoordinator _gridCoordinator;
        private TilePool _tilePool;
        private BlankTilesLevelSetup _blankTilesLevelSetup;
        private MatchFinder _matchFinder;
        
        private bool _isDebugging;
        
       

        public void InitializeBoard()
        {
            GridWidth = _levelConfiguration.LevelGridWidth;
            GridHeight = _levelConfiguration.LevelGridHeight;;
            _grid = new GridSystem(GridWidth, GridHeight,_gridCoordinator);
            _blankTilesLevelSetup.Generate(_levelConfiguration);
         
            FillBoard();
            while (_matchFinder.CheckBoard(this, _gridCoordinator)) 
                FillBoard();
        }

        private void FillBoard()
        {
            RefillBoard();
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    if (_blankTilesLevelSetup.Blanks[x, y])
                    {
                        var tile = _tilePool.CreateBlankTile(_gridCoordinator.GridToWorld(x, y), transform);
                        Grid.SetValue(x, y, tile);
                    }
                    else
                    {
                        var tile = _tilePool.GetTileFromPool(_gridCoordinator.GridToWorld(x, y), transform);
                        Grid.SetValue(x, y, tile);
                        _tilesToRefill.Add(tile);
                    }
                }
            }
        }

        private void RefillBoard()
        {
            if (_tilesToRefill == null) return;
            foreach (var potion in _tilesToRefill)
            {
                Grid.SetValue(potion.GameObject().transform.position, null);
                potion.GameObject().SetActive(false);
            }
            _tilesToRefill.Clear();
        }

       [Inject] private void Construct(GridCoordinator gridCoordinator, TilePool tilePool, BlankTilesLevelSetup blankTilesLevelSetup, MatchFinder matchFinder)
        {
            _gridCoordinator = gridCoordinator;
            _blankTilesLevelSetup = blankTilesLevelSetup;
            _tilePool = tilePool;
            _matchFinder = matchFinder;
        }
    }
}