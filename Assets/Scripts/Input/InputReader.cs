using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputReader : IDisposable
    {
        public event Action Click;
        
        private readonly PlayerInput _playerInput;
        private InputAction _positionAction;
        private InputAction _fireAction;
        
        private bool _isFire;

        public InputReader(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _positionAction = _playerInput.actions["Select"];
            _fireAction = _playerInput.actions["Fire"];
            _fireAction.performed += OnClick;
        }

        public void Dispose() => _fireAction.performed -= OnClick;

        public Vector2 Position => _positionAction.ReadValue<Vector2>();

        private void OnClick(InputAction.CallbackContext context) => 
            Click?.Invoke();
        
    }
}