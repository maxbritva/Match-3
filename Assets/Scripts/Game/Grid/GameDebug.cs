using Game.Board;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Grid
{
    public class GameDebug
    {
        private GridCoordinator _gridCoordinator;
        private BoardInteraction _boardInteraction;
        public void DrawDebugLines(int width, int height, float cellSize, Vector3 origin)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    CreateDebugText(_boardInteraction.transform, x + "," + y,_gridCoordinator.GridToWorldCenter(x, y, cellSize, origin),
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
            TMP.fontSize = 2f;
            TMP.color = Color.white;
            TMP.alignment = TextAlignmentOptions.Center;
        }
        
        [Inject] private void Construct(GridCoordinator gridCoordinator, DiContainer diContainer, BoardInteraction boardInteraction)
        {
            _gridCoordinator = gridCoordinator;
            _boardInteraction = boardInteraction;
        }

    }
}