using System;
using UnityEngine;

public abstract class Ability : MonoBehaviour, IUpdate
{
    public event Action<IUpdate> UpdateRemoveRequested;
    
    public Effect Effect;
    public AbilityType Type;
    public float Duration;
    
    public virtual void GameUpdate(float deltaTime)
    {
    }
}