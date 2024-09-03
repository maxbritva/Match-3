using UnityEngine;
using Zenject;

namespace Game
{
    public class BoardView : MonoBehaviour
    {
        private Board _board;
        
        [Inject] private void Construct(Board board)
        {
            _board = board;
        }
    }
}