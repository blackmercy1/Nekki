using Game.Common.Filters;
using UnityEngine;

namespace Game.Common.Configs.Entity.Warrior
{
    [CreateAssetMenu(fileName = "WarriorConfig", menuName = "ScriptableObject/Configs/WarriorConfig")]
    public sealed class WarriorConfig : ScriptableObject
    {
        [SerializeField] private Game.Entity.Warrior _prefab;
        [SerializeField] private Team _team;
    
        // TODO - можно сделать int(можно взять даже тип поменьше, если нам не нужны космические значения)
        // кастомной структорой с валидацией, т.к наши значения с высокой долей вероятности не будут уходить в минус
        //а также выделить отдельный клас для всех этих статов, но они будут разница от сущности к сущности
        [Header("Stats Values")]
        [SerializeField] private int _healthValue;
        [SerializeField] private int _damageValue;
        [SerializeField] private int _defenceValue;
        [SerializeField] private int _movementSpeedValue;

        public Game.Entity.Warrior Prefab => _prefab;
        public Team Team => _team;
    
        public int HealthValue => _healthValue;
        public int DamageValue => _damageValue;
        public int DefenceValue => _defenceValue;
        public int MovementSpeedValueValue => _movementSpeedValue;
    }
}