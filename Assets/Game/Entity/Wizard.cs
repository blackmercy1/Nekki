using System;
using Game.Common.Collision;
using Game.Common.Filters;
using Game.Common.Stats;
using Game.Core.Input;
using Game.Core.Input.Updates;
using Game.Systems.AttackSystem;
using Game.Systems.MovementSystem;
using UnityEngine;

namespace Game.Entity
{
    public sealed class Wizard : MonoBehaviour, IDamageable, IUpdate, IMember
    {
        public event Action<IUpdate> UpdateRemoveRequested;

        public Team TeamMember => _teamMember;
    
        private CollisionComponent _collisionComponent;
        private IMovementHandler _movementHandler;
        private IPlayerInputHandler _inputHandler;
        private ITypeStat<int> _healthStat;

        private IAttack _attack;
        private ITypeStat<int> _defenceStat;
        private IStats<int> _stats;
    
        private Team _teamMember;
    
        public void Initialize(
            Team teamMember,
            CollisionComponent collisionComponent,
            IStats<int> stats,
            IMovementHandler movementHandler,
            IPlayerInputHandler inputHandler,
            MeleeAttack attack)
        {
            _teamMember = teamMember;
            _collisionComponent = collisionComponent;
            _stats = stats;
            _movementHandler = movementHandler;
        
            _inputHandler = inputHandler;
            _attack = attack;
        
            _healthStat = _stats.Get(StatsConstantIdentifiers.HealthStat);
            _defenceStat = stats.Get(StatsConstantIdentifiers.DefenceStat);
        
            collisionComponent.CollisionReaction += Attack;
        }
    

        private void Attack(GameObject obj)
        {
            _attack.Attack(obj);
        }

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
            _movementHandler.Move(deltaTime);
        }
    }
}