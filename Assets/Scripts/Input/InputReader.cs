using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Input
{
    public class InputReader : IDisposable
    {
        public event Action Click;
        
        private readonly PlayerInputs _playerInputs;
        private InputAction _positionAction;
        private InputAction _fireAction;
        private IObjectResolver _objectResolver;
        
        private bool _isFire;

        public InputReader()
        {
            _playerInputs = new PlayerInputs();
            //  _positionAction = _playerInput.actions["Select"];
           // _fireAction = _playerInput.actions["Fire"];
           _playerInputs.Player.Fire.performed += OnClick;
        }
        public void EnableInputs(bool value)
        {
            if(value)
                _playerInputs. Enable();
            else
                _playerInputs.Disable();
        }
        public void Dispose() => _playerInputs.Player.Fire.performed -= OnClick;

        public Vector2 Position =>  _playerInputs.Player.Select.ReadValue<Vector2>();

        private void OnClick(InputAction.CallbackContext context) => 
            Click?.Invoke();
        
    }
}