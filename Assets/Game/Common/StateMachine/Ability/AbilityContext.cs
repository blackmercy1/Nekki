using System.Collections.Generic;
using System.Linq;
using Game.Common.Stats;
using Game.Core.Input;
using Game.Core.Input.Updates;
using Game.Systems.AbilitySystem;

namespace Game.Common.StateMachine.Ability
{
    public class AbilityContext : IStateSwitcher
    {
        private readonly GameUpdates _gameUpdates;
        private readonly List<AbilityBaseState> _allStates;
    
        private AbilityBaseState _currentState;

        public AbilityContext(
            AbilityHolder abilityHolder,
            IPlayerInputHandler playerInputHandler, 
            GameUpdates gameUpdates)
        {
            _gameUpdates = gameUpdates;
            _allStates = new List<AbilityBaseState>
            {
                new IdleState(this, playerInputHandler),
                new CastAbilityState(this, abilityHolder, _gameUpdates),
                new SetNextSpellState(this, abilityHolder),
                new SetPreviousSpellState(this, abilityHolder)
            };
        
            _currentState = _allStates[0];
            _currentState.Start();
        }
    
        public void SwitchState<T>() where T : AbilityBaseState
        {
            _currentState.Stop();
            _currentState = _allStates.FirstOrDefault(t => t is T);
            _currentState.Start();
        }
    }
}