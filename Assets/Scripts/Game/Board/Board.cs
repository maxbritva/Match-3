using Game.Grid;
using Game.Tiles;
using UnityEngine;
using GridSystem = Game.Grid.GridSystem;

namespace Game.Board
{
    public class Board : MonoBehaviour
    {
        private GridSystem _grid;
        public GridSystem Grid => _grid;

        public Board(GridCoordinator gridCoordinator, GameDebug gameDebug)
        {
            _gridCoordinator = gridCoordinator;
            _gameDebug = gameDebug;
        }

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
            if(_gridCoordinator == null)
                Debug.Log(11);
            _gridWidth = 10;
            _gridHeight = 10;
            _cellSize = 1f;
            _originPosition = Vector3.zero;
            if(_isDebugging)
                _gameDebug.DrawDebugLines(_gridWidth, _gridHeight, _cellSize, _originPosition);
            _grid = new GridSystem(_gridWidth, _gridHeight, _cellSize, _originPosition, _isDebugging, _gridCoordinator);
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
    }
}