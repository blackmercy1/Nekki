using System;
using UnityEngine;

public interface IPlayerInputHandler : IInputHandler
{
    event Action AbilityActivated;
    event Action AbilityNextActivated;
    event Action AbilityPreviousActivated;
    Vector3 PlayerInputDirection { get; }
}