using UnityEngine;

public class ForwardMovement : IMovementHandler
{
    private readonly Transform _transform;
    private readonly ITypeStat<int> _movementSpeed;

    public ForwardMovement(
        Transform transform,
        ITypeStat<int> movementSpeed)
    {
        _transform = transform;
        _movementSpeed = movementSpeed;
    }
    
    public void Move(float fixedDeltaTime)
    {
        _transform.position += Vector3.forward * (fixedDeltaTime * _movementSpeed.GetValue());
    }
}