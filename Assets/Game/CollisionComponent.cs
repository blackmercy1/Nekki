using System;
using UnityEngine;

public sealed class CollisionComponent : MonoBehaviour
{
    public event Action<GameObject> CollisionReaction;

    private void OnCollisionEnter(Collision collision) => CollisionReaction?.Invoke(collision.gameObject);
}