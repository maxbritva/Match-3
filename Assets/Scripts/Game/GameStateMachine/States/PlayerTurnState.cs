using System;
using Animations;
using Audio;
using Game.Grid;
using Game.Tiles;
using Input;
using UnityEngine;

namespace Game.GameStateMachine.States
{
    public class PlayerTurnState: IState, IDisposable
    {
        private readonly Vector2Int _emptyPosition = Vector2Int.one * -1;
        private readonly InputReader _inputReader;
        private readonly GridSystem _grid;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly Camera _camera;
        private readonly AudioManager _audioManager;
        private readonly IAnimation _animation;

        public PlayerTurnState(GridSystem grid, IStateSwitcher stateSwitcher, AudioManager audioManager, IAnimation animation)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _audioManager = audioManager;
            _animation = animation;
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
                _audioManager.PlayClick();
                _grid.SetCurrentPosition(clickPosition);
                _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1.2f);
            }

            else if (_grid.CurrentPosition == clickPosition)
            {
                _audioManager.PlayDeselect();
                DeselectTile();
            }

            else if(_grid.CurrentPosition  != clickPosition  && IsSwappable(_grid.CurrentPosition, clickPosition))
            {
                _grid.SetTargetPosition(clickPosition);
                _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1f);
                _stateSwitcher.SwitchState<SwapTilesState>();
            }
        }

        private bool IsSwappable(Vector2Int currentTile, Vector2Int targetTile) => 
            Mathf.Abs(currentTile.x - targetTile.x) + Mathf.Abs(currentTile.y - targetTile.y) == 1;

        private void DeselectTile()
        {
            _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1f);
            _grid.SetCurrentPosition(_emptyPosition);
            _grid.SetTargetPosition(_emptyPosition);
        }
        private bool IsBlankPosition(Vector2Int gridPosition) => _grid.GetValue(gridPosition.x, gridPosition.y).tileType.TileKind == TileKind.Blank;
        private bool IsValidPosition(Vector2 gridPosition) => 
            gridPosition.x >= 0 && gridPosition.x < _grid.Width && gridPosition.y >= 0 && gridPosition.y < _grid.Height;
        
    }
}