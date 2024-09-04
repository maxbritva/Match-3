using System.Collections;
using DG.Tweening;
using Game.Grid;
using Input;
using UnityEngine;
using Zenject;

namespace Game.Board
{
    public class BoardInteraction : MonoBehaviour
    {
        private Board _board;
        private GridCoordinator _gridCoordinator;
        private InputReader _inputReader;
        private Vector2Int _selectedTile = Vector2Int.one * -1;

        private void OnEnable() => _inputReader.Fire += OnSelectTile;

        private void OnDisable() => _inputReader.Fire -= OnSelectTile;
        

        private void OnSelectTile()
        {
            var gridPosition = _gridCoordinator.WorldToGrid(Camera.main.ScreenToWorldPoint(_inputReader.Selected), _board.CellSize, _board.OriginPosition);
            if (IsValidPosition(gridPosition) == false || IsEmptyPosition(gridPosition)) return;
            if (_selectedTile == gridPosition) 
            {
                DeselectGem();
                //audioManager.PlayDeselect();
            } 
            else if (_selectedTile == Vector2Int.one * -1)
            {
                SelectGem(gridPosition);
                // audioManager.PlayClick();
            }
            else
                StartCoroutine(SwapGems(_selectedTile, gridPosition)) ;
               // StartCoroutine(RunGameLoop(gridPosition, gridPosition));
        }
        
        
        IEnumerator RunGameLoop(Vector2Int gridPosA, Vector2Int gridPosB)
        {
            yield return StartCoroutine(SwapGems(gridPosA, gridPosB));
            
            // Matches?
            // List<Vector2Int> matches = FindMatches();
            // // TODO: Calculate score
            // // Make Gems explode
            // yield return StartCoroutine(ExplodeGems(matches));
            // // Make gems fall
            // yield return StartCoroutine(MakeGemsFall());
            // // Fill empty spots
            // yield return StartCoroutine(FillEmptySpots());
            
            // TODO: Check if game is over

            DeselectGem();
        }
        
        IEnumerator SwapGems(Vector2Int gridPosA, Vector2Int gridPosB) 
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
            
            yield return new WaitForSeconds(0.5f);
        }
        
        private void DeselectGem() => _selectedTile = new Vector2Int(-1, -1);
        private void SelectGem(Vector2Int gridPosition) => _selectedTile = gridPosition;

        private bool IsEmptyPosition(Vector2Int gridPosition) => 
            _board.Grid.GetValue(gridPosition.x, gridPosition.y) == null;

        private bool IsValidPosition(Vector2 gridPosition) => 
            gridPosition.x >= 0 && gridPosition.x < _board.GridWidth && gridPosition.y >= 0 && gridPosition.y < _board.GridHeight;

        [Inject] private void Construct(Board board, InputReader inputReader, GridCoordinator gridCoordinator)
        {
            _board = board;
            _inputReader = inputReader;
            _gridCoordinator = gridCoordinator;
        }
    }
}