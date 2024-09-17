using UnityEngine;

namespace Game.Utils
{
    public class SetupCamera
    {
        private bool _isVertical;
        
        public SetupCamera(bool isVertical) => _isVertical = isVertical;

        public void SetCamera(int width, int height)
        {
            var xPos = width / 2f;
            var yPos = height/ 2f + 0.5f;
            Camera.main.gameObject.transform.position = new Vector3(xPos, yPos, -10f);
            Camera.main.orthographicSize = GetOrthographicSize(width, height);
        }
        
        private float GetOrthographicSize(int width, int height) =>
            _isVertical ? (width + 1f) * Screen.height / Screen.width * 0.5f : 
                (height + 1f) * Screen.height / Screen.width;
    }
}