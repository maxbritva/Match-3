using Game.Grid;
using Game.Tiles;
using UnityEngine;
using Zenject;
using GridSystem = Game.Grid.GridSystem;

namespace Game.Board
{
    public class Board : MonoBehaviour
    {
        private GridSystem _grid;
        public GridSystem Grid => _grid;

        public int GridWidth => _gridWidth;
        public int GridHeight => _gridHeight;
        public float CellSize => _cellSize;
        public Vector3 OriginPosition => _originPosition;

        private int _gridWidth;
        private GridCoordinator _gridCoordinator;
        private TileCreator _tileCreator;
        private int _gridHeight;
        private float _cellSize;
        private bool _isDebugging;
        private Vector3 _originPosition;
        private GameDebug _gameDebug;

        private void Start()
        {
            _gridWidth = 10;
            _gridHeight = 10;
            _cellSize = 1f;
            _originPosition = Vector3.zero;
            if(_isDebugging)
                _gameDebug.DrawDebugLines(_gridWidth, _gridHeight, _cellSize, _originPosition);
            _grid = new GridSystem(_gridWidth, _gridHeight, _cellSize, _originPosition, _isDebugging);
           InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++) 
                    _tileCreator.CreateTile(x,y,_gridCoordinator.GridToWorldCenter(x, y, _cellSize, _originPosition), transform);
            }
        }

        [Inject] private void Construct(GridCoordinator gridCoordinator, GameDebug gameDebug, BoardInteraction boardInteraction, TileCreator tileCreator)
        {
            _gridCoordinator = gridCoordinator;
            _gameDebug = gameDebug;
            _tileCreator = tileCreator;
        }
    }
}