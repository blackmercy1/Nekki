using Game.Common.Filters;
using UnityEngine;

namespace Game.Common.Configs.Entity.Wizard
{
    [CreateAssetMenu(fileName = "WizardConfig", menuName = "ScriptableObject/Configs/WizardConfig")]
    public sealed class WizardConfig : ScriptableObject
    {
        [Header("Stats Values")]
        [SerializeField] private int _healthValue;
        [SerializeField] private int _damageValue;
        [SerializeField] private int _defenceValue;
        [SerializeField] private int _movementSpeedValue;
        [SerializeField] private int _rotationSpeedValue;
    
        [SerializeField] private Game.Entity.Wizard _prefab;
        [SerializeField] private Team _team;

        public Game.Entity.Wizard Prefab => _prefab;
        public int HealthValue => _healthValue;
        public int DamageValue => _damageValue;
        public int DefenceValue => _defenceValue;
        public int MovementSpeedValue => _movementSpeedValue;
        public int RotationSpeedValue => _rotationSpeedValue;
    
        public Team Team => _team;
    }
}