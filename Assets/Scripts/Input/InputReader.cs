using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _selectAction;
        private InputAction _fireAction;

        public event Action Fire;

        public Vector2 Selected => _selectAction.ReadValue<Vector2>();

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _selectAction = _playerInput.actions["Select"];
            _fireAction = _playerInput.actions["Fire"];

            _fireAction.performed += OnFire;
        }

        private void OnDestroy() => _fireAction.performed -= OnFire;

        public void OnFire(InputAction.CallbackContext context) => 
            Fire?.Invoke();
    }
}