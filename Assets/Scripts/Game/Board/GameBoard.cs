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

        private GridSystem _grid;
        private GridCoordinator _gridCoordinator;
        private TileCreator _tileCreator;
        private GenerateBlankTiles _generateBlankTiles;
        private MatchFinder _matchFinder;
        
        private bool _isDebugging;
       private List<Tile> _potionsToDestroy = new List<Tile>();
      

        private void Start()
        {
         
        }

        public void InitializeBoard()
        {
          
            GridWidth = _levelConfiguration.LevelGridWidth;
            GridHeight = _levelConfiguration.LevelGridHeight;;
            _grid = new GridSystem(GridWidth, GridHeight,_gridCoordinator);
            _generateBlankTiles.Generate(_levelConfiguration);
            Debug.Log("Grid was created");
            CreateTiles();

            while (_matchFinder.CheckBoard(this, _gridCoordinator))
            {
                CreateTiles();
            }
        }

        private void CreateTiles()
        {
            DestroyPotions();
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    if (_generateBlankTiles.Blanks[x, y])
                    {
                        var tile = _tileCreator.CreateBlankTile(_gridCoordinator.GridToWorld(x, y), transform);
                        Grid.SetValue(x, y, tile);
                        Debug.Log("blank was created");
                    }
                    else
                    {
                        var tile = _tileCreator.CreateTile(_gridCoordinator.GridToWorld(x, y), transform);
                        Grid.SetValue(x, y, tile);
                        _potionsToDestroy.Add(tile);
                        Debug.Log("tile was created");
                    }
                }
            }

            Debug.Log("Board was created");
        }

        private void DestroyPotions()
        {
            if (_potionsToDestroy == null) return;
            foreach (var potion in _potionsToDestroy)
            {
                var value = Grid.GetValue(potion.GameObject().transform.position);
                Grid.SetValue(potion.GameObject().transform.position, null);
                Destroy(potion);
            }
               
            _potionsToDestroy.Clear();
        }

       [Inject] private void Construct(GridCoordinator gridCoordinator, TileCreator tileCreator, GenerateBlankTiles blankTiles, MatchFinder matchFinder)
        {
            _gridCoordinator = gridCoordinator;
            _generateBlankTiles = blankTiles;
            _tileCreator = tileCreator;
            _matchFinder = matchFinder;
        }
    }
}