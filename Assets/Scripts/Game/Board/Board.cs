using Game.Grid;
using Game.Tiles;
using UnityEngine;
using VContainer;
using GridSystem = Game.Grid.GridSystem;

namespace Game.Board
{
    public class Board : MonoBehaviour
    {
        public GridSystem Grid => _grid;
        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        public float CellSize { get; private set; }
        public Vector3 OriginPosition { get; private set; }

        private GridSystem _grid;
        private GridCoordinator _gridCoordinator;
        private TileCreator _tileCreator;
        private bool _isDebugging;
        private GameDebug _gameDebug;

        private void Start()
        {
            GridWidth = 10;
            GridHeight = 10;
            CellSize = 1f;
            OriginPosition = Vector3.zero;
            if(_isDebugging)
                _gameDebug.ShowDebugGrid(GridWidth, GridHeight, CellSize, OriginPosition, transform);
            _grid = new GridSystem(GridWidth, GridHeight, CellSize, OriginPosition,_gridCoordinator);
           InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    var tile = _tileCreator.CreateTile(x,y,
                        _gridCoordinator.GridToWorldCenter(x, y, CellSize, OriginPosition), transform);
                    Grid.SetValue(x,y, tile);
                }
            }
        }

       [Inject] private void Construct(GridCoordinator gridCoordinator, GameDebug gameDebug, TileCreator tileCreator)
        {
            _gridCoordinator = gridCoordinator;
            _gameDebug = gameDebug;
            _tileCreator = tileCreator;
        }
    }
}