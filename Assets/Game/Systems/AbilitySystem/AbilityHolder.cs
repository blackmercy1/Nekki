using System.Collections.Generic;
using Game.Systems.AbilitySystem.Creator;

namespace Game.Systems.AbilitySystem
{
    public class AbilityHolder
    {
        private readonly List<AbilityCreator> _abilityCreators;

        private AbilityCreator _currentAbilityCreator;
        private int _currentAbilityIndex;

        public AbilityHolder(List<AbilityCreator> abilityCreators)
        {
            _abilityCreators = abilityCreators;
            _currentAbilityCreator = abilityCreators[0];
            _currentAbilityIndex = 0;
        }

        public Ability GetSpell() => _currentAbilityCreator.CreateAbility();

        public void SetNextAbility()
        {
            if (_currentAbilityIndex + 1 > _abilityCreators.Count - 1)
            {
                ChangeIndexAbility(0);
            }

            ChangeIndexAbility(_currentAbilityIndex + 1);
        }

        public void SetPreviousAbility()
        {
            if (_currentAbilityIndex - 1 < 0)
            {
                ChangeIndexAbility(_abilityCreators.Count - 1);
            }

            ChangeIndexAbility(_currentAbilityIndex - 1);
        }

        private void ChangeIndexAbility(int index)
        {
            _currentAbilityIndex = index;
            _currentAbilityCreator = _abilityCreators[_currentAbilityIndex];
        }
    }
}