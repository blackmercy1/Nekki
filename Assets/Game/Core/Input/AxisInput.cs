namespace Game.Core.Input
{
    public class AxisInput : IAxisInput
    {
        private readonly string _axisName;

        public AxisInput(string axisName)
        {
            _axisName = axisName;
        }
        
        public float GetAxis() => UnityEngine.Input.GetAxis(_axisName);
    }
}