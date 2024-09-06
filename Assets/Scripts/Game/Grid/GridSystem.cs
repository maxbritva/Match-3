using System;
using Game.Board;
using Game.Tiles;
using UnityEngine;
using VContainer;

namespace Game.Grid
{
    public class GridSystem
    {
        public event Action<int, int, Tile> OnValueChanged; 
        
        private int _width;
        private int _height;
        private float _CellSize;
        private Vector3 _origin;
        private Tile[,] _gridArray;

        private GridCoordinator _gridCoordinator;
     

        public GridSystem(int width, int height, float cellSize, Vector3 origin, bool isDebugSession, GridCoordinator gridCoordinator)
        {
            _width = width;
            _height = height;
            _CellSize = cellSize;
            _origin = origin;
            _gridArray = new Tile[width, height];
            _gridCoordinator = gridCoordinator;

            // if (isDebugSession) _gameDebug.DrawDebugLines(_width, _height, cellSize, _origin);
        }

        public bool IsValid(int x, int y) => x >= 0 && y >= 0 && x < _width && y < _height;

        public void SetValue(int x, int y, Tile value)
        {
            if (IsValid(x, y))
                _gridArray[x, y] = value;
            OnValueChanged?.Invoke(x, y, value);
        }

        public void SetValue(Vector3 worldPosition, Tile value)
        {
            Vector2Int position = _gridCoordinator.WorldToGrid(worldPosition, _CellSize, _origin);
            SetValue(position.x,position.y,value);
        }

        public Tile GetValue(Vector3 worldPosition)
        {
            Vector2Int position = GetXY(worldPosition);
            return GetValue(position.x, position.y);
        }
        
        public Tile GetValue(int x, int y) => IsValid(x, y) ? _gridArray[x, y] : default;

        public Vector2Int GetXY(Vector3 worldPosition) => _gridCoordinator.WorldToGrid(worldPosition, _CellSize, _origin);
        
 
    }
}
