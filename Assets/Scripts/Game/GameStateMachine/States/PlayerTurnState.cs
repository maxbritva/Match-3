using System;
using DG.Tweening;
using Game.Grid;
using Game.Tiles;
using Input;
using UnityEngine;

namespace Game.GameStateMachine.States
{
    public class PlayerTurnState: IState, IDisposable
    {
        private readonly Vector2Int _emptyPosition = Vector2Int.one * -1;
        private InputReader _inputReader;
        private GridSystem _grid;
        private IStateSwitcher _stateSwitcher;
        private Camera _camera;

        public PlayerTurnState(GridSystem grid, IStateSwitcher stateSwitcher)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _camera = Camera.main;
            _inputReader = new InputReader();
            _inputReader.Click += OnTileClick;
        }
               
        public void Dispose() => _inputReader.Click -= OnTileClick;

        public void Enter()
        {
            _inputReader.EnableInputs(true);
            DeselectTile();
        }

        public void Exit() => _inputReader.EnableInputs(false);

        private void OnTileClick()
        {
            var clickPosition = _grid.WorldToGrid(_camera.ScreenToWorldPoint(_inputReader.Position));
            
            if (IsValidPosition(clickPosition) == false || IsBlankPosition(clickPosition))
                return;

            if (_grid.CurrentPosition == _emptyPosition)
            {
                _grid.SetCurrentPosition(clickPosition);
                AnimateSelectionTile( _grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1.2f);
            }

            else if (_grid.CurrentPosition == clickPosition)
                DeselectTile();
            
            else if(_grid.CurrentPosition  != clickPosition  && IsSwappable(_grid.CurrentPosition, clickPosition))
            {
                _grid.SetTargetPosition(clickPosition);
                AnimateSelectionTile( _grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1f);
                _stateSwitcher.SwitchState<SwapTilesState>();
            }
            
        }
        
        private void AnimateSelectionTile(Tile tile, float value) => tile.transform.DOScale(value, 0.3f).SetEase(Ease.OutCubic);

        private bool IsSwappable(Vector2Int currentTile, Vector2Int targetTile) => 
            Mathf.Abs(currentTile.x - targetTile.x) + Mathf.Abs(currentTile.y - targetTile.y) == 1;

        private void DeselectTile()
        {
            AnimateSelectionTile( _grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1f);
            _grid.SetCurrentPosition(_emptyPosition);
            _grid.SetTargetPosition(_emptyPosition);
        }
        private bool IsBlankPosition(Vector2Int gridPosition) => _grid.GetValue(gridPosition.x, gridPosition.y).tileType.TileKind == TileKind.Blank;
        private bool IsValidPosition(Vector2 gridPosition) => 
            gridPosition.x >= 0 && gridPosition.x < _grid.Width && gridPosition.y >= 0 && gridPosition.y < _grid.Height;
        
    }
}