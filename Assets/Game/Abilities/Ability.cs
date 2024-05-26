using System;
using Game.Core.Updates;
using UnityEngine;

namespace Game.Abilities
{
    public abstract class Ability : MonoBehaviour, IUpdate
    {
        public event Action<IUpdate> UpdateRemoveRequested;
        public AbilityType Type;
        public float Duration;
    
        public virtual void GameUpdate(float deltaTime)
        {
        }

        protected virtual void OnDestroy()
        {
            UpdateRemoveRequested?.Invoke(this);
        }
    }
}