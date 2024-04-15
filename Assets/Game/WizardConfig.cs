using UnityEngine;

[CreateAssetMenu(fileName = "WizardConfig", menuName = "ScriptableObject/Configs/WizardConfig")]
public sealed class WizardConfig : ScriptableObject
{
    [SerializeField] private Team _team;
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private int _defenceValue;
    [SerializeField] private int _movementSpeedValue;

    public int HealthValue => _healthValue;
    public int DamageValue => _damageValue;
    public int DefenceValue => _defenceValue;
    public int MovementSpeedValue => _movementSpeedValue;
    
    public Team Team => _team;
}