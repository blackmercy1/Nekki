using Game.Common.Stats;
using Game.Systems.AbilitySystem;

namespace Game.Common.StateMachine.Ability
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