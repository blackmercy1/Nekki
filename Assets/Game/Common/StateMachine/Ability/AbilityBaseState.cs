using Game.Common.Stats;

namespace Game.Common.StateMachine.Ability
{
    public abstract class AbilityBaseState
    {
        protected readonly IStateSwitcher _stateSwitcher;

        protected AbilityBaseState(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public virtual void Start()
        {
        
        }

        public virtual void Stop()
        {
        }
    }
}