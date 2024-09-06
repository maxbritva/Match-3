using Game.Grid;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Game.Board
{
    public class BoardInteraction: MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Board _board;
        private GameLoop _gameLoop;
        private GridCoordinator _gridCoordinator;
        private InputReader _inputReader;
        private Vector2Int _selectedTile = Vector2Int.one * -1;

        private void OnEnable()
        {
            _playerInput = GetComponent<PlayerInput>();
            _inputReader = new InputReader(_playerInput);
            _inputReader.Click += OnSelectTile;
        }

        private void OnDisable() => _inputReader.Click -= OnSelectTile;
        
        private async void OnSelectTile()
        {
            var gridPosition = _gridCoordinator.WorldToGrid(Camera.main.ScreenToWorldPoint(_inputReader.Position), _board.CellSize, _board.OriginPosition);
            if (IsValidPosition(gridPosition) == false || IsEmptyPosition(gridPosition))
            {
                Debug.Log(11);
                return;
            }
              
            if (_selectedTile == gridPosition) 
            {
                Debug.Log(11);
                DeselectTile();
                //audioManager.PlayDeselect();
            } 
            else if (_selectedTile == Vector2Int.one * -1)
            {
                Debug.Log(11);
                SelectTile(gridPosition);
                // audioManager.PlayClick();
            }
            else
            {
                Debug.Log(11);
                await _gameLoop.RunGameLoop(_selectedTile, gridPosition);
            }
              
        }
        
        private void DeselectTile() => _selectedTile = new Vector2Int(-1, -1);
        private void SelectTile(Vector2Int gridPosition) => _selectedTile = gridPosition;

        private bool IsEmptyPosition(Vector2Int gridPosition) => 
            _board.Grid.GetValue(gridPosition.x, gridPosition.y) == null;

        private bool IsValidPosition(Vector2 gridPosition) => 
            gridPosition.x >= 0 && gridPosition.x < _board.GridWidth && gridPosition.y >= 0 && gridPosition.y < _board.GridHeight;

        [Inject] private void Construct(GridCoordinator gridCoordinator, GameLoop gameLoop, Board board)
        {
            _gameLoop = gameLoop;
            _gridCoordinator = gridCoordinator;
            _board = board;
        }
    }
}