using Game.Common.Stats;
using Game.Core.Input;
using UnityEngine;

namespace Game.Systems.MovementSystem
{
    public class PlayerMovementHandler : IMovementHandler
    {
        private readonly IPlayerInputHandler _inputHandler;
        private readonly Transform _transform;
        private readonly ITypeStat<int> _movementSpeed;
        private readonly ITypeStat<int> _rotationSpeed;

        public PlayerMovementHandler(
            IPlayerInputHandler inputHandler,
            Transform transform,
            ITypeStat<int> movementSpeed,
            ITypeStat<int> rotationSpeed)
        {
            _inputHandler = inputHandler;
            _transform = transform;
            _movementSpeed = movementSpeed;
            _rotationSpeed = rotationSpeed;
        }
    
        public void Move(float fixedDeltaTime)
        {
            var input = _inputHandler.PlayerInputDirection;
            input.Normalize();
            _transform.Translate(input * (_movementSpeed.GetValue() * fixedDeltaTime), Space.World);

            if (input != Vector3.zero)
            {
                var toRotation = Quaternion.LookRotation(input, Vector3.up);

                _transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation,
                    _rotationSpeed.GetValue() * fixedDeltaTime);
            }
        }
    }
}