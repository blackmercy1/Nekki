using Game.Common.Stats;
using Game.Core.Input;

namespace Game.Abilities.States
{
    public class IdleState : AbilityBaseState
    {
        private readonly IButtonInput _activateAbilityButton;
        private readonly IButtonInput _activateNextAbilityButton;
        private readonly IButtonInput _activatePreviousAbilityButton;

        public IdleState(
            IStateSwitcher stateSwitcher,
            IButtonInput activateAbilityButton,
            IButtonInput activateNextAbilityButton,
            IButtonInput activatePreviousAbilityButton)
            : base(stateSwitcher)
        {
            _activateAbilityButton = activateAbilityButton;
            _activateNextAbilityButton = activateNextAbilityButton;
            _activatePreviousAbilityButton = activatePreviousAbilityButton;
        }
        
        public override void GameUpdate(float deltaTime)
        {
            if (_activateAbilityButton.IsButtonDown())
                Cast();
            else if (_activateNextAbilityButton.IsButtonDown())
                SetNextSpellState();
            else if (_activatePreviousAbilityButton.IsButtonDown())
                SetPreviousSpellState();
        }
        private void SetNextSpellState() => StateSwitcher.SwitchState<SetNextSpellState>();
        private void SetPreviousSpellState() => StateSwitcher.SwitchState<SetPreviousSpellState>();
        private void Cast() => StateSwitcher.SwitchState<CastAbilityState>();
    }
}