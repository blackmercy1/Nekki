using Game.Abilities.States;

namespace Game.Common.Stats
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : AbilityBaseState;
    }
}