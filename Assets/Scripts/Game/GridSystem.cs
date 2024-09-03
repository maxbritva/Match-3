using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GridSystem<T>
    {
        public event Action<int, int, T> OnValueChanged; 
        
        private int _width;
        private int _height;
        private float _CellSize;
        private Vector3 _origin;
        private T[,] _gridArray;

        private Coordinator _coordinator;
        private GameDebug _gameDebug;
        private BoardView _boardView;

        public GridSystem(int width, int height, float cellSize, Vector3 origin, bool isDebugSession)
        {
            _width = width;
            _height = height;
            _CellSize = cellSize;
            _origin = origin;
            _gridArray = new T[width, height];

            // if (isDebugSession) _gameDebug.DrawDebugLines(_width, _height, cellSize, _origin);

        }

        public bool IsValid(int x, int y) => x >= 0 && y >= 0 && x < _width && y < _height;

        public void SetValue(int x, int y, T value)
        {
            if (IsValid(x, y))
                _gridArray[x, y] = value;
            OnValueChanged?.Invoke(x, y, value);
        }

        public void SetValue(Vector3 worldPosition, T value)
        {
            Vector2Int position = _coordinator.WorldToGrid(worldPosition, _CellSize, _origin);
            SetValue(position.x,position.y,value);
        }

        public T GetValue(Vector3 worldPosition)
        {
            Vector2Int position = GetXY(worldPosition);
            return GetValue(position.x, position.y);
        }
        
        public T GetValue(int x, int y) => IsValid(x, y) ? _gridArray[x, y] : default;

        public Vector2Int GetXY(Vector3 worldPosition) => _coordinator.WorldToGrid(worldPosition, _CellSize, _origin);
        
        [Inject] private void Construct(Coordinator coordinator, GameDebug gameDebug, BoardView boardView)
        {
            _coordinator = coordinator;
            _gameDebug = gameDebug;
            _boardView = boardView;
        }
    }
}
