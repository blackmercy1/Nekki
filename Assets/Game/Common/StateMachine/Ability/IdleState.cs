using Game.Common.Stats;
using Game.Core.Input;
using UnityEngine;

namespace Game.Common.StateMachine.Ability
{
    public class IdleState : AbilityBaseState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IPlayerInputHandler _playerInputHandler;

        public IdleState(
            IStateSwitcher stateSwitcher,
            IPlayerInputHandler playerInputHandler)
            : base(stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
            _playerInputHandler = playerInputHandler;
        }

        public override void Start()
        {
            Debug.LogError("подписка");
            _playerInputHandler.AbilityActivated += Cast;
            _playerInputHandler.AbilityNextActivated += SetNextSpellState;
            _playerInputHandler.AbilityPreviousActivated += SetPreviousSpellState;
        }
    
        public override void Stop()
        {
            Debug.LogError("атписка");
            _playerInputHandler.AbilityActivated -= Cast;
            _playerInputHandler.AbilityNextActivated -= SetNextSpellState;
            _playerInputHandler.AbilityPreviousActivated -= SetPreviousSpellState;
        }
    
        private void SetNextSpellState() => _stateSwitcher.SwitchState<SetNextSpellState>();
        private void SetPreviousSpellState() => _stateSwitcher.SwitchState<SetPreviousSpellState>();
        private void Cast() => _stateSwitcher.SwitchState<CastAbilityState>(); 
    }
}