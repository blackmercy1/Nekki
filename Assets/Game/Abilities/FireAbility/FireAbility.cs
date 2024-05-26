using Game.Attack;
using Game.Common.Collision;
using Game.Common.Stats;
using Game.Movement;
using UnityEngine;

namespace Game.Abilities.FireAbility
{
    public class FireAbility : Ability
    {
        private CollisionComponent _collisionComponent;
        private IMovement _movement;
        private Stats _stats;
        private IAttack _attackComponent;

        public void Initialize(
            float duration,
            IMovement movement, 
            Stats stats,
            CollisionComponent collisionComponent,
            IAttack attack)
        {
            Duration = duration;
            _movement = movement;
            _stats = stats;
            _collisionComponent = collisionComponent;
            _attackComponent = attack;

            _collisionComponent.CollisionReaction += Collision;
        }
    
        public override void GameUpdate(float deltaTime) => _movement.Move(deltaTime);

        private void Collision(GameObject obj) => Attack(obj);

        private void Attack(GameObject obj)
        {
            if (_attackComponent.Attack(obj))
                DestroySelf();
        }

        private void DestroySelf() => Destroy(gameObject);
    }
}