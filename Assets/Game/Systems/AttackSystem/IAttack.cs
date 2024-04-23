using UnityEngine;

namespace Game.Systems.AttackSystem
{
    public interface IAttack
    {
        bool Attack(GameObject gameObject);
    }
}