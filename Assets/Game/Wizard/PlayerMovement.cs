using Game.Common.Stats;
using Game.Core.Input;
using Game.Movement;
using UnityEngine;

namespace Game.Wizard
{
    public class PlayerMovement : IMovement
    {
        private readonly IDirectionInput _directionInput;
        private readonly Transform _transform;
        private readonly ITypeStat<int> _movementSpeed;
        private readonly ITypeStat<int> _rotationSpeed;

        public PlayerMovement(
            IDirectionInput directionInput,
            Transform transform,
            ITypeStat<int> movementSpeed,
            ITypeStat<int> rotationSpeed)
        {
            _directionInput = directionInput;
            _transform = transform;
            _movementSpeed = movementSpeed;
            _rotationSpeed = rotationSpeed;
        }
    
        public void Move(float fixedDeltaTime)
        {
            var direction = _directionInput.GetDirection();

            var offset = _movementSpeed.GetValue() * fixedDeltaTime;
            _transform.Translate(direction * offset, Space.World);

            if (direction == Vector3.zero) 
                return;
            
            var toRotation = Quaternion.LookRotation(direction, Vector3.up);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation,
                _rotationSpeed.GetValue() * fixedDeltaTime);
        }
    }
}