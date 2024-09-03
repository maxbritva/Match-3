using Game.Tiles;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Board : MonoBehaviour
    {
        private GridSystem<GridTile<Tile>> _grid;
        public GridSystem<GridTile<Tile>> Grid => _grid;

        private int _gridWidth;
        private Coordinator _coordinator;
        private TileCreator _tileCreator;
        private int _gridHeight;
        private float _cellSize;
        private bool _isDebugging;
        private Vector3 _originPosition;
        private GameDebug _gameDebug;

        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private TileType[] _tileTypes;
        private void Start()
        {
            _gridWidth = 10;
            _gridHeight = 10;
            _cellSize = 1f;
            _originPosition = Vector3.forward;
            if(_isDebugging)
                _gameDebug.DrawDebugLines(_gridWidth, _gridHeight, _cellSize, _originPosition);
            _grid = new GridSystem<GridTile<Tile>>(_gridWidth, _gridHeight, _cellSize, _originPosition, _isDebugging);
           InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++) 
                    _tileCreator.CreateTile(x,y,_coordinator.GridToWorldCenter(x, y, _cellSize, _originPosition), transform);
            }
        }

        [Inject] private void Construct(Coordinator coordinator, GameDebug gameDebug, BoardView boardView, TileCreator tileCreator)
        {
            _coordinator = coordinator;
            _gameDebug = gameDebug;
            _tileCreator = tileCreator;
        }
    }
}