using TMPro;
using UnityEngine;

namespace Game.Grid
{
    public class GameDebug
    {
        private GridSystem _grid;
        public GameDebug(GridSystem gridCoordinator) => _grid = gridCoordinator;

        public void ShowDebugGrid(int width, int height, Transform parent)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++) 
                    CreateDebugText(parent, x + "," + y,_grid.GridToWorld(x, y));
            }
        }

        private void CreateDebugText(Transform parent, string text, Vector3 position)
        {
            GameObject TMPGameObject = new GameObject("DebugText" + text, typeof(TextMeshPro));
            TMPGameObject.transform.parent = parent;
            TMPGameObject.transform.position = position;
            TMPGameObject.transform.forward = Vector3.forward;
            var TMP = TMPGameObject.GetComponent<TextMeshPro>();
            TMP.text = text;
            TMP.fontSize = 3f;
            TMP.color = Color.white;
            TMP.alignment = TextAlignmentOptions.Center;
        }
    }
}