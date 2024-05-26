using Game.Common.Filters;
using UnityEngine;

namespace Game.Warrior
{
    [CreateAssetMenu(fileName = "WarriorConfig", menuName = "ScriptableObject/Configs/WarriorConfig")]
    public sealed class WarriorConfig : ScriptableObject
    {
        [SerializeField] private Warrior _prefab;
        [SerializeField] private Team _team;
        
        [Header("Stats Values")]
        [SerializeField] private int _healthValue;
        [SerializeField] private int _damageValue;
        [SerializeField] private int _defenceValue;
        [SerializeField] private int _movementSpeedValue;

        public Warrior Prefab => _prefab;
        public Team Team => _team;
    
        public int HealthValue => _healthValue;
        public int DamageValue => _damageValue;
        public int DefenceValue => _defenceValue;
        public int MovementSpeedValueValue => _movementSpeedValue;
    }
}