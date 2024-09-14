using System;
using Game.Tiles;
using UnityEngine;

namespace Game.Grid
{
    public class GridSystem
    {
        public event Action<int, int, Tile> OnValueChanged; 
        public Tile[,] Grid { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Vector2Int CurrentPosition { get; private set; }
        public Vector2Int TargetPosition { get; private set; }
        
        public void SetupGrid(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new Tile[width, height];
        }

        public Vector2Int SetCurrentPosition(Vector2Int value) => CurrentPosition = value;
        public Vector2Int SetTargetPosition(Vector2Int value) => TargetPosition = value;
        
        public Vector3 GridToWorld(int x, int y) => 
            new Vector3(x + 0.5f, y  + 0.5f, 0);

        public Vector2Int WorldToGrid(Vector3 worldPosition)
        {
            var x = Mathf.FloorToInt(worldPosition.x);
            var y = Mathf.FloorToInt(worldPosition.y);
            return new Vector2Int(x,y);
        }

        public void SetValue(int x, int y, Tile value)
        {
            if (IsValid(x, y))
                Grid[x, y] = value;
            OnValueChanged?.Invoke(x, y, value);
        }
        
        public void SetValue(Vector3 worldPosition, Tile value)
        {
            Vector2Int position = WorldToGrid(worldPosition);
            SetValue(position.x,position.y,value);
        }

        public Tile GetValue(Vector3 worldPosition)
        {
            Vector2Int position = WorldToGrid(worldPosition);
            return GetValue(position.x, position.y);
        }
        
        public Tile GetValue(int x, int y) => IsValid(x, y) ? Grid[x, y] : default;
        
        public bool IsValid(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;
    }
}
