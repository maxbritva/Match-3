using System.Collections;
using DG.Tweening;
using Game.Grid;
using Input;
using UnityEngine;
using VContainer;
using VContainer;

namespace Game.Board
{
    public class BoardInteraction : MonoBehaviour
    {
        private Board _board;
        private GameLoop _gameLoop;
        private GridCoordinator _gridCoordinator;
        private InputReader _inputReader;
        private Vector2Int _selectedTile = Vector2Int.one * -1;

        public BoardInteraction(Board board, GameLoop gameLoop, GridCoordinator gridCoordinator, InputReader inputReader)
        {
            _board = board;
            _gameLoop = gameLoop;
            _gridCoordinator = gridCoordinator;
            _inputReader = inputReader;
        }

        private void OnEnable() => _inputReader.Fire += OnSelectTile;

        private void OnDisable() => _inputReader.Fire -= OnSelectTile;
        

        private async void OnSelectTile()
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
                await _gameLoop.RunGameLoop(_selectedTile, gridPosition);
        }
        
        
       
        
        private void DeselectGem() => _selectedTile = new Vector2Int(-1, -1);
        private void SelectGem(Vector2Int gridPosition) => _selectedTile = gridPosition;

        private bool IsEmptyPosition(Vector2Int gridPosition) => 
            _board.Grid.GetValue(gridPosition.x, gridPosition.y) == null;

        private bool IsValidPosition(Vector2 gridPosition) => 
            gridPosition.x >= 0 && gridPosition.x < _board.GridWidth && gridPosition.y >= 0 && gridPosition.y < _board.GridHeight;

        // [Inject] private void Construct(InputReader inputReader, GridCoordinator gridCoordinator, GameLoop gameLoop, Board board)
        // {
        //     _board = board;
        //     _inputReader = inputReader;
        //     _gridCoordinator = gridCoordinator;
        // }
    }
}