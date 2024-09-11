using UnityEngine;

namespace Game.Utils
{
    public class SetupCamera
    {
        private Board.Board _board;
        protected bool _isVertical;
        
        public SetupCamera(Board.Board board, bool isVertical)
        {
            _board = board;
            _isVertical = isVertical;
            MoveCamera();
        }

        private void MoveCamera()
        {
            int xPos = _board.GridWidth / 2;
            int yPos = _board.GridHeight/ 2;
            Camera.main.gameObject.transform.position = new Vector3(xPos, yPos, -10f);
            Camera.main.orthographicSize = GetOthographicSize();
        }
        
        private float GetOthographicSize() =>
            _isVertical ? (_board.GridWidth + 2f) * Screen.height / Screen.width * 0.5f : 
                (_board.GridWidth + 2f) * Screen.height / Screen.width;
    }
}