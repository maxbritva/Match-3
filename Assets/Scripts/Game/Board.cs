using Game.Tiles;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Board : MonoBehaviour
    {
        private GridSystem<GridTile<Gem>> _grid;
        private int _gridWidth;
        private Coordinator _coordinator;
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
            _originPosition = Vector3.forward;
            _isDebugging = true;
            _grid = new GridSystem<GridTile<Gem>>(_gridWidth, _gridHeight, _cellSize, _originPosition, _isDebugging);
            _gameDebug.DrawDebugLines(_gridWidth, _gridHeight, _cellSize, _originPosition);
        }

        [Inject] private void Construct(Coordinator coordinator, GameDebug gameDebug, BoardView boardView)
        {
            _coordinator = coordinator;
            _gameDebug = gameDebug;
        }
    }
}