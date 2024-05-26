using Game.Common.Filters;
using UnityEngine;

namespace Game.Wizard
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
    
        [SerializeField] private Wizard _prefab;
        [SerializeField] private Team _team;
        
        [field: SerializeField] public string XAxisName { get; private set; }
        [field: SerializeField] public string YAxisName { get; private set; }
        [field: SerializeField] public KeyCode ActivateAbilityButton { get; private set; }
        [field: SerializeField] public KeyCode ActivateNextAbilityButton { get; private set; }
        [field: SerializeField] public KeyCode ActivatePreviousAbilityButton { get; private set; }

        public Wizard Prefab => _prefab;
        public int HealthValue => _healthValue;
        public int DamageValue => _damageValue;
        public int DefenceValue => _defenceValue;
        public int MovementSpeedValue => _movementSpeedValue;
        public int RotationSpeedValue => _rotationSpeedValue;
    
        public Team Team => _team;
    }
}