using Game.Common.Stats;
using Game.Systems.AbilitySystem;

namespace Game.Common.StateMachine.Ability
{
    public class SetPreviousSpellState : AbilityBaseState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly AbilityHolder _holder;

        public SetPreviousSpellState(
            IStateSwitcher stateSwitcher,
            AbilityHolder holder) : 
            base(stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
            _holder = holder;
        }

        public override void Start()
        {
            _holder.SetPreviousAbility();
            _stateSwitcher.SwitchState<IdleState>();
        }
    }
}