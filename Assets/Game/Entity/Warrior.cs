using System;
using Game.Common.Collision;
using Game.Common.Filters;
using Game.Common.Stats;
using Game.Core.Input.Updates;
using Game.Systems.AttackSystem;
using Game.Systems.MovementSystem;
using UnityEngine;

namespace Game.Entity
{
    public sealed class Warrior : MonoBehaviour, IDamageable, IMember, IUpdate
    {
        public event Action<IUpdate> UpdateRemoveRequested;

        public Team TeamMember => _teamMemberMember;
    
        private CollisionComponent _collisionComponent;
        private ITypeStat<int> _defenceStat;
        private ITypeStat<int> _healthStat;
        private IStats<int> _stats;
    
        private IMovementHandler _movementHandler;
        private IAttack _attackComponent;
    
        private Team _teamMemberMember;

        public void Initialize(
            Team team,
            CollisionComponent collisionComponent,
            IStats<int> stats,
            MoveToTarget movementHandler,
            IAttack attackComponent)
        {
            _teamMemberMember = team;
            _collisionComponent = collisionComponent;
            _stats = stats;
            _movementHandler = movementHandler;
            _attackComponent = attackComponent;

            _healthStat = _stats.Get(StatsConstantIdentifiers.HealthStat);
            _defenceStat = stats.Get(StatsConstantIdentifiers.DefenceStat);

            _collisionComponent.CollisionReaction += Attack;
        }

        private void Attack(GameObject obj)
        {
            if (_attackComponent.Attack(obj))
                DestroySelf();
        }

        public void TakeDamage(int damagePoints)
        {
            _healthStat.Add(-damagePoints * _defenceStat.GetValue());

            if (_healthStat.GetValue() <= 0)
                DestroySelf();
        }
    
        public void GameUpdate(float deltaTime)
        {
            _movementHandler.Move(deltaTime);
        }
    
        private void DestroySelf()
        {
            UpdateRemoveRequested?.Invoke(this);
            Destroy(gameObject);
        }
    }
}