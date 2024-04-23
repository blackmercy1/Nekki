using Game.Common.Stats;
using UnityEngine;

namespace Game.Systems.MovementSystem
{
    public class MoveToTarget : IMovementHandler
    {
        private readonly Transform _ownTransform;
        private readonly Transform _targetTransform;
        private readonly ITypeStat<int> _movementSpeed;

        public MoveToTarget(
            Transform ownTransform,
            Transform targetTransform,
            ITypeStat<int> movementSpeed)
        {
            _ownTransform = ownTransform;
            _targetTransform = targetTransform;
            _movementSpeed = movementSpeed;
        }
    
        public void Move(float fixedDeltaTime)
        {
            _ownTransform.position = Vector3.MoveTowards(
                _ownTransform.position,
                _targetTransform.position,
                _movementSpeed.GetValue() * fixedDeltaTime);
        }
    }
}