using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class CollisionComponent : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    
    private Action<GameObject> _collisionReaction;

    public void Init(Action<GameObject> collisionReaction)
    {
        _collisionReaction = collisionReaction;
    }

    private void OnCollisionEnter(Collision collision) => _collisionReaction?.Invoke(collision.gameObject);
}