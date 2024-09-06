using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Grid;
using Input;
using UnityEngine;

namespace Game
{
    public class GameLoop
    {
        private Board.Board _board;
        private GridCoordinator _gridCoordinator;
        
        public async UniTask RunGameLoop(Vector2Int gridPosA, Vector2Int gridPosB)
        {
             Debug.Log(1111);
            //  UniTask.WaitUntil(await SwapTiles(gridPosA, gridPosB));

            //DeselectGem();
        }
        
        public async UniTask SwapTiles(Vector2Int gridPosA, Vector2Int gridPosB)
        {
            var gridObjectA = _board.Grid.GetValue(gridPosA.x, gridPosA.y);
            var gridObjectB =  _board.Grid.GetValue(gridPosB.x, gridPosB.y);
            Debug.Log(gridObjectA.transform.position);
            Debug.Log(gridObjectB.transform.position);
            gridObjectA.transform
                .DOLocalMove( _gridCoordinator.GridToWorldCenter(gridPosB.x, gridPosB.y, _board.CellSize, _board.OriginPosition), 0.5f)
                .SetEase(Ease.InQuad);
            gridObjectB.transform
                .DOLocalMove(_gridCoordinator.GridToWorldCenter(gridPosA.x, gridPosA.y,_board.CellSize, _board.OriginPosition), 0.5f)
                .SetEase(Ease.InQuad);
            
            _board.Grid.SetValue(gridPosA.x, gridPosA.y, gridObjectB);
            _board.Grid.SetValue(gridPosB.x, gridPosB.y, gridObjectA);
            
            //yield return new WaitForSeconds(0.5f);
        }
        //
        //
        // [Inject] private void Construct(Board.Board board, GridCoordinator gridCoordinator)
        // {
        //     _board = board;
        //     _gridCoordinator = gridCoordinator;
        // }

    }
}