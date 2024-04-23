using Game.Common.Stats;
using UnityEngine;

namespace Game.Systems.MovementSystem
{
    public class ForwardMovement : IMovementHandler
    {
        private readonly Transform _transform;
        private readonly ITypeStat<int> _movementSpeed;
        private readonly Quaternion _holderRotation;

        public ForwardMovement(
            Transform transform,
            ITypeStat<int> movementSpeed, 
            Quaternion holderRotation)
        {
            _transform = transform;
            _movementSpeed = movementSpeed;
            _holderRotation = holderRotation;
        }
    
        public void Move(float fixedDeltaTime)
        {
            _transform.rotation = _holderRotation;
            _transform.position += _transform.forward * (fixedDeltaTime * _movementSpeed.GetValue());
        }
    }
}