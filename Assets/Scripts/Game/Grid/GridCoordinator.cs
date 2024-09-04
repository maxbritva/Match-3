using UnityEngine;

namespace Game.Grid
{
    public class GridCoordinator
    {
        public Vector3 GridToWorldPosition(int x, int y, float cellSize, Vector3 origin) => 
            new Vector3(x, y, 0) * cellSize + origin;

        public Vector3 GridToWorldCenter(int x, int y, float cellSize, Vector3 origin) => 
            new Vector3(x * cellSize + cellSize * 0.5f, y * cellSize + cellSize * 0.5f, 0) * cellSize + origin;

        public Vector2Int WorldToGrid(Vector3 worldPosition, float CellSize, Vector3 origin)
        {
            Vector3 gridPosition = (worldPosition - origin) / CellSize;
            var x = Mathf.FloorToInt(gridPosition.x);
            var y = Mathf.FloorToInt(gridPosition.y);
            return new Vector2Int(x,y);
        }

        public Vector3 forward => Vector3.forward;
    }
}