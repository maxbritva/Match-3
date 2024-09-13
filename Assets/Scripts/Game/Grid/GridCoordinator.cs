using UnityEngine;

namespace Game.Grid
{
    public class GridCoordinator
    {
        public Vector3 GridToWorld(int x, int y) => 
            new Vector3(x + 0.5f, y  + 0.5f, 0);

        public Vector2Int WorldToGrid(Vector3 worldPosition)
        {
            var x = Mathf.FloorToInt(worldPosition.x);
            var y = Mathf.FloorToInt(worldPosition.y);
            return new Vector2Int(x,y);
        }
        
        public Vector3 forward => Vector3.forward;
    }
}