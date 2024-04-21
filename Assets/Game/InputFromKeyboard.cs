using System;
using UnityEngine;

public class InputFromKeyboard : IPlayerInputHandler
{
    public event Action<IUpdate> UpdateRemoveRequested;
    
    public event Action AbilityActivated;
    public event Action AbilityNextActivated;
    public event Action AbilityPreviousActivated;
    public Vector3 PlayerInputDirection => _playerInputDirection;
    
    private Vector3 _playerInputDirection;

    private void GetMovementInput()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        _playerInputDirection = new Vector3(horizontalInput, 0, verticalInput);
    }

    private void GetAbilityInput()
    {
        if (AbilityActivated == null)
            return;
        if (Input.GetKeyDown(KeyCode.X))
            AbilityActivated?.Invoke();
    }

    private void GetAbilitySwapInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            AbilityPreviousActivated?.Invoke();
        if (Input.GetKeyDown(KeyCode.E))
            AbilityNextActivated?.Invoke();
    }

    public void GameUpdate(float deltaTime)
    {
        GetAbilitySwapInput();
        GetMovementInput();
        GetAbilityInput();
    }
}