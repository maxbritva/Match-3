using UnityEngine;

namespace Game.Utils
{
    public class SetupCamera
    {
        private bool _isVertical;
        
        public SetupCamera(bool isVertical) => _isVertical = isVertical;

        public void SetCamera(int width, int height)
        {
            int xPos = width / 2;
            int yPos = height/ 2;
            Camera.main.gameObject.transform.position = new Vector3(xPos, yPos, -10f);
            Camera.main.orthographicSize = GetOrthographicSize(width, height);
        }
        
        private float GetOrthographicSize(int width, int height) =>
            _isVertical ? (width + 2f) * Screen.height / Screen.width * 0.5f : 
                (height + 2f) * Screen.height / Screen.width;
    }
}