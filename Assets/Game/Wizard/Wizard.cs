using System;
using Game.Abilities;
using Game.Attack;
using Game.Common.Collision;
using Game.Common.Filters;
using Game.Common.Stats;
using Game.Core.Updates;
using Game.Movement;
using UnityEngine;

namespace Game.Wizard
{
    public sealed class Wizard : MonoBehaviour, IDamageable, IUpdate, IMember
    {
        public event Action<IUpdate> UpdateRemoveRequested;

        public Team TeamMember => _teamMember;
    
        private CollisionComponent _collisionComponent;
        private IMovement _movement;
        private IAttack _attack;
        private AbilityContext _abilityContext;

        private ITypeStat<int> _healthStat;
        private ITypeStat<int> _defenceStat;
        private IStats<int> _stats;
        private Team _teamMember;

        public void Initialize(
            Team teamMember,
            CollisionComponent collisionComponent,
            IStats<int> stats,
            IMovement movement,
            MeleeAttack attack, 
            AbilityContext abilityContext)
        {
            _teamMember = teamMember;
            _collisionComponent = collisionComponent;
            _stats = stats;
            _movement = movement;
            
            _attack = attack;
            _abilityContext = abilityContext;
        
            _healthStat = _stats.Get(StatsConstantIdentifiers.HealthStat);
            _defenceStat = stats.Get(StatsConstantIdentifiers.DefenceStat);
        
            collisionComponent.CollisionReaction += Attack;

            _abilityContext.Start();
        }

        private void OnEnable() => _abilityContext?.Start();
        private void OnDisable() => _abilityContext?.Stop();
        private void Attack(GameObject obj) => _attack.Attack(obj);

        public void TakeDamage(int damagePoints)
        {
            _healthStat.Add(-damagePoints * _defenceStat.GetValue());

            if (_healthStat.GetValue() <= 0)
                DestroySelf();
        }

        private void DestroySelf()
        {
            UpdateRemoveRequested?.Invoke(this);
            _collisionComponent.CollisionReaction -= Attack;
            Destroy(gameObject);
        }

        public void GameUpdate(float deltaTime)
        {
            _movement.Move(deltaTime);
            _abilityContext.GameUpdate(deltaTime);
        }
    }
}