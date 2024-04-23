using UnityEngine;

namespace Game.Positions
{
    public class GenerateConstantPosition : IPositionGenerator
    {
        private readonly Vector3 _staticPosition;

        public GenerateConstantPosition(Vector3 staticPosition)
        {
            _staticPosition = staticPosition;
        }
    
        public Vector3 GeneratePosition()
        {
            return _staticPosition;
        }
    }
}