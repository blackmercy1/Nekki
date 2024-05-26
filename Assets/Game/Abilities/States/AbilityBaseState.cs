using Game.Common.Stats;

namespace Game.Abilities.States
{
    public abstract class AbilityBaseState
    {
        protected readonly IStateSwitcher StateSwitcher;

        protected AbilityBaseState(IStateSwitcher stateSwitcher)
        {
            StateSwitcher = stateSwitcher;
        }

        public virtual void GameUpdate(float deltaTime) { }

        public virtual void Start() { }

        public virtual void Stop() { }
    }
}