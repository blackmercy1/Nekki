using System.Collections.Generic;
using System.Linq;
using Game.Abilities.States;
using Game.Common.Stats;
using Game.Core.Input;
using Game.Core.Updates;

namespace Game.Abilities
{
    public class AbilityContext : IStateSwitcher
    {
        private readonly List<AbilityBaseState> _allStates;
    
        private AbilityBaseState _currentState;
        
        public AbilityContext(
            AbilityHolder abilityHolder,
            GameUpdates gameUpdates,
            IButtonInput activateAbilityButton,
            IButtonInput activateNextAbilityButton,
            IButtonInput activatePreviousAbilityButton)
        {
            _allStates = new List<AbilityBaseState>
            {
                new IdleState(
                    this,
                    activateAbilityButton,
                    activateNextAbilityButton,
                    activatePreviousAbilityButton),
                
                new CastAbilityState(
                    this,
                    abilityHolder,
                    gameUpdates),
                
                new SetNextSpellState(
                    this,
                    abilityHolder),
                
                new SetPreviousSpellState(
                    this,
                    abilityHolder)
            };
            _currentState = _allStates[0];
        }

        public void Start() => _currentState.Start();
        public void Stop() => _currentState.Stop();

        public void GameUpdate(float deltaTime)
        {
            _currentState?.GameUpdate(deltaTime);
        }

        public void SwitchState<T>() where T : AbilityBaseState
        {
            _currentState.Stop();
            _currentState = _allStates.FirstOrDefault(t => t is T);
            _currentState.Start();
        }
    }
}