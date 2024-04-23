using System;
using Game.Core.Input.Updates;
using UnityEngine;

namespace Game.Systems.AbilitySystem
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