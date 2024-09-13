using Game.Grid;
using Input;
using UnityEngine;
using VContainer;

namespace Game.Board
{
    public class BoardInteractions: MonoBehaviour
    {
        private readonly Vector2Int _emptyPosition = Vector2Int.one * -1;
        private Vector2Int _selectedTile;
        private InputReader _inputReader;
        private GridSystem _grid;
        private Camera _camera;

        private void OnEnable()
        {
            _camera = Camera.main;
            _inputReader = new InputReader();
            _inputReader.Click += OnTileClick;
        }
        public void EnablePlayerInputs(bool value) => _inputReader.EnableInputs(value);

        private void OnDisable() => _inputReader.Click -= OnTileClick;
        
        private async void OnTileClick()
        {
            var selectedPosition = _grid.WorldToGrid(_camera.ScreenToWorldPoint(_inputReader.Position));
            if (_selectedTile == _emptyPosition && IsValidPosition(selectedPosition))
            {
                SelectTile(selectedPosition);
            }
            else
            {
                DeselectTile();
                //audioManager.PlayDeselect();
            }
            if (_selectedTile == selectedPosition) return;
            
            await _gameLoop.RunGameLoop(_selectedTile, selectedPosition);
        }
        
        
        // private async void OnTileClick()
        // {
        //     var selectedPosition = _grid.WorldToGrid(_camera.ScreenToWorldPoint(_inputReader.Position));
        //     if
        //     
        //     if (IsValidPosition(selectedPosition) == false || IsEmptyPosition(selectedPosition))
        //         return;
        //
        //     if (_selectedTile == selectedPosition) 
        //     {
        //         DeselectTile();
        //         //audioManager.PlayDeselect();
        //     } 
        //     else if (_selectedTile == _emptyPosition)
        //     {
        //         SelectTile(selectedPosition);
        //         // audioManager.PlayClick();
        //     }
        //     else
        //     {
        //         Debug.Log(44);
        //         await _gameLoop.RunGameLoop(_selectedTile, selectedPosition);
        //     }
        // }

        
        private void DeselectTile() => _selectedTile = _emptyPosition;
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