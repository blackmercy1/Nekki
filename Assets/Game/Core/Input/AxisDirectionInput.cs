using UnityEngine;

namespace Game.Core.Input
{
    public class AxisDirectionInput : IDirectionInput
    {
        private readonly IAxisInput _xAxisInput;
        private readonly IAxisInput _yAxisInput;
        private readonly IAxisInput _zAxisInput;

        public AxisDirectionInput(
            IAxisInput xAxisInput,
            IAxisInput yAxisInput,
            IAxisInput zAxisInput)
        {
            _xAxisInput = xAxisInput;
            _yAxisInput = yAxisInput;
            _zAxisInput = zAxisInput;
        }
        
        public Vector3 GetDirection()
        {
            var vector = new Vector3(
                _xAxisInput.GetAxis(),
                _yAxisInput.GetAxis(),
                _zAxisInput.GetAxis());
            return vector.normalized;
        }
    }
}