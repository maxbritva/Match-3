using TMPro;
using UnityEngine;

namespace Game.Grid
{
    public class GameDebug
    {
        private GridCoordinator _gridCoordinator;

        public GameDebug(GridCoordinator gridCoordinator) => _gridCoordinator = gridCoordinator;

        public void ShowDebugGrid(int width, int height, float cellSize, Vector3 origin, Transform parent)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    CreateDebugText(parent, x + "," + y,_gridCoordinator.GridToWorldCenter(x, y, cellSize, origin),
                        _gridCoordinator.forward);
                }
            }
        }

        private void CreateDebugText(Transform parent, string text, Vector3 position, Vector3 direction)
        {
            GameObject TMPGameObject = new GameObject("DebugText" + text, typeof(TextMeshPro));
            TMPGameObject.transform.parent = parent;
            TMPGameObject.transform.position = position;
            TMPGameObject.transform.forward = direction;
            var TMP = TMPGameObject.GetComponent<TextMeshPro>();
            TMP.text = text;
            TMP.fontSize = 3f;
            TMP.color = Color.white;
            TMP.alignment = TextAlignmentOptions.Center;
        }
    }
}