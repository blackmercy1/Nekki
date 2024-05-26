namespace Game.Core.Input
{
    public interface IAxisInput
    {
        float GetAxis();
    }
    
    public class EmptyAxisInput : IAxisInput
    {
        public float GetAxis() => 0;
    }
}