using System;
using Game.Core.Updates;
using UnityEngine;

namespace Game.Core.Input
{
    public class InputFromKeyboard : IPlayerInputHandler
    {
        public event Action<IUpdate> UpdateRemoveRequested;
    
        public event Action AbilityActivated;
        public event Action AbilityNextActivated;
        public event Action AbilityPreviousActivated;
        public Vector3 PlayerInputDirection => _playerInputDirection;
    
        private Vector3 _playerInputDirection;

        public void GameUpdate(float deltaTime)
        {
            GetMovementInput();
            GetAbilityInput();
            GetAbilitySwapInput();
        }
        
        private void GetMovementInput()
        {
            var horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
            var verticalInput = UnityEngine.Input.GetAxis("Vertical");
            _playerInputDirection = new Vector3(horizontalInput, 0, verticalInput);
        }

        private void GetAbilityInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.X))
                AbilityActivated?.Invoke();
        }

        private void GetAbilitySwapInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
                AbilityPreviousActivated?.Invoke();
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
                AbilityNextActivated?.Invoke();
        }
    }
}