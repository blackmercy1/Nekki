using Game.Core.Input.Updates;

namespace Game.Core.Input
{
    public interface IInputHandler : IUpdate
    {
        void GetInput(){}
    }
}