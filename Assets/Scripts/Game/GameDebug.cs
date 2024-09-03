using TMPro;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameDebug
    {
        private Coordinator _coordinator;
        private BoardView _boardView;
        public void DrawDebugLines(int width, int height, float cellSize, Vector3 origin)
        {
            const float duration = 100f;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    CreateDebugText(_boardView.transform, x + "," + y,_coordinator.GridToWorldCenter(x, y, cellSize, origin),
                        _coordinator.forward);
                    // Debug.DrawLine(_coordinator.GridToWorldPosition(x,y,cellSize,origin),  
                    //     _coordinator.GridToWorldPosition(x,y + 1,cellSize,origin), Color.white, duration);
                    // Debug.DrawLine(_coordinator.GridToWorldPosition(x,y,cellSize,origin),  
                    //     _coordinator.GridToWorldPosition(x + 1,y,cellSize,origin), Color.white, duration);
                }
               
            }
            // Debug.DrawLine(_coordinator.GridToWorldPosition(0,height,cellSize,origin),  
            //     _coordinator.GridToWorldPosition(width,height,cellSize,origin), Color.white, duration);
            // Debug.DrawLine(_coordinator.GridToWorldPosition(width,0,cellSize,origin),  
            //     _coordinator.GridToWorldPosition(width,height,cellSize,origin), Color.white, duration);
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
        
        [Inject] private void Construct(Coordinator coordinator, DiContainer diContainer, BoardView boardView)
        {
            _coordinator = coordinator;
            _boardView = boardView;
        }

    }
}