using System;
using UnityEngine;

public sealed class Wizard : IDamageable, IUpdate
{
    public event Action<IUpdate> UpdateRemoveRequested;
    
    private readonly Team _teams;
    private readonly CollisionComponent _collisionComponent;
    private readonly IStats<int> _stats;
    private readonly GameObject _gameObject;
    private readonly IInputHandler _inputHandler;

    public Wizard(
        Team team,
        CollisionComponent collisionComponent,
        IStats<int> stats,
        GameObject gameObject,
        IInputHandler inputHandler)
    {
        _collisionComponent = collisionComponent;
        _teams = team;
        _stats = stats;
        _gameObject = gameObject;
        _inputHandler = inputHandler;
    }
    
    public void TakeDamage(int damagePoints)
    {
        
    }

    public void GameUpdate(float deltaTime)
    {
        _inputHandler.GetInput();
    }
}

public interface IInputHandler
{
    Vector3 PlayerInputDirection { get; }
    void GetInput(){}
}