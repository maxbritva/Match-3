using Game.Grid;
using Input;
using UnityEngine;
using VContainer;

namespace Game.Board
{
    public class BoardInteraction: MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private GridSystem _grid;
        private InputReader _inputReader;
        private Vector2Int _selectedTile = Vector2Int.one * -1;

        private void OnEnable()
        {
            _inputReader = new InputReader();
            _inputReader.Click += OnSelectTile;
        }

        private void OnDisable() => _inputReader.Click -= OnSelectTile;
        
        private async void OnSelectTile()
        {
            var gridPosition = _grid.WorldToGrid(_camera.ScreenToWorldPoint(_inputReader.Position));
            if (IsValidPosition(gridPosition) == false || IsEmptyPosition(gridPosition))
            {
                return;
            }
              
            if (_selectedTile == gridPosition) 
            {
                DeselectTile();
                //audioManager.PlayDeselect();
            } 
            else if (_selectedTile == Vector2Int.one * -1)
            {
                SelectTile(gridPosition);
                // audioManager.PlayClick();
            }
            else
            {
                Debug.Log(44);
            //    await _gameLoop.RunGameLoop(_selectedTile, gridPosition);
                DeselectTile();
            }
        }
        
        private void DeselectTile() => _selectedTile = new Vector2Int(-1, -1);
        private void SelectTile(Vector2Int gridPosition) => _selectedTile = gridPosition;

        private bool IsEmptyPosition(Vector2Int gridPosition) => 
            _grid.GetValue(gridPosition.x, gridPosition.y) == null;

        private bool IsValidPosition(Vector2 gridPosition) => 
            gridPosition.x >= 0 && gridPosition.x < _grid.Width && gridPosition.y >= 0 && gridPosition.y < _grid.Height;

        [Inject] private void Construct(GridSystem grid)
        {
            _grid = grid;
        }
    }
}