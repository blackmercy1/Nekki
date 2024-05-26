using Game.Core.Updates;

namespace Game.Core.Input
{
    public interface IInputHandler : IUpdate
    {
        void GetInput(){}
    }
}