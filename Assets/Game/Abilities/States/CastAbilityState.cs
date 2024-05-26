using Game.Common.Stats;
using Game.Core.Updates;

namespace Game.Abilities.States
{
    public class CastAbilityState : AbilityBaseState
    {
        private readonly AbilityHolder _abilityHolder;
        private readonly GameUpdates _gameUpdates;

        public CastAbilityState(
            IStateSwitcher stateSwitcher,
            AbilityHolder abilityHolder,
            GameUpdates gameUpdates) 
            : base(stateSwitcher)
        {
            _abilityHolder = abilityHolder;
            _gameUpdates = gameUpdates;
        }

        public override void Start()
        {
            var ability = _abilityHolder.GetSpell();
            _gameUpdates.Add(ability);
            StateSwitcher.SwitchState<IdleState>();
        }

        public override void Stop()
        {
        }
    }
}