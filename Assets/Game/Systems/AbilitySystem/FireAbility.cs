using System;
using Game.Common.Collision;
using Game.Common.Stats;
using Game.Core.Input.Updates;
using Game.Systems.AttackSystem;
using Game.Systems.MovementSystem;
using UnityEngine;

namespace Game.Systems.AbilitySystem
{
    public class FireAbility : Ability
    {
        public event Action<IUpdate> UpdateRemoveRequested;
    
        private CollisionComponent _collisionComponent;
        private IMovementHandler _movementHandler;
        private Stats _stats;
        private IAttack _attackComponent;

        public void Initialize(
            float duration,
            IMovementHandler movementHandler, 
            Stats stats,
            CollisionComponent collisionComponent,
            IAttack attack)
        {
            Duration = duration;
            _movementHandler = movementHandler;
            _stats = stats;
            _collisionComponent = collisionComponent;
            _attackComponent = attack;

            _collisionComponent.CollisionReaction += Collision;
        }
    
        public override void GameUpdate(float deltaTime)
        {
            _movementHandler.Move(deltaTime);
        }

        private void Collision(GameObject obj)
        {
            Attack(obj);
        }

        private void Attack(GameObject obj)
        {
            if (_attackComponent.Attack(obj))
                DestroySelf();
        }

        private void DestroySelf()
        {
            UpdateRemoveRequested?.Invoke(this);
            Destroy(gameObject);
        }
    }
}