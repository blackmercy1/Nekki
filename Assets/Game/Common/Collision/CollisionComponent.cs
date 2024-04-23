using System;
using UnityEngine;

namespace Game.Common.Collision
{
    public sealed class CollisionComponent : MonoBehaviour
    {
        public event Action<GameObject> CollisionReaction;

        private void OnCollisionEnter(UnityEngine.Collision collision) => CollisionReaction?.Invoke(collision.gameObject);
    }
}