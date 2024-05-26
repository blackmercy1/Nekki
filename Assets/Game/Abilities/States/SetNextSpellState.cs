using Game.Common.Stats;

namespace Game.Abilities.States
{
    public class SetNextSpellState : AbilityBaseState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly AbilityHolder _holder;

        public SetNextSpellState(
            IStateSwitcher stateSwitcher,
            AbilityHolder holder) : 
            base(stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
            _holder = holder;
        }

        public override void Start()
        {
            _holder.SetNextAbility();
            _stateSwitcher.SwitchState<IdleState>();
        }
    }
}