using Game.Common.StateMachine.Ability;

namespace Game.Common.Stats
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : AbilityBaseState;
    }
}