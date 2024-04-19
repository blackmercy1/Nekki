using UnityEngine;

[CreateAssetMenu(fileName = "WizardConfig", menuName = "ScriptableObject/Configs/WizardConfig")]
public sealed class WizardConfig : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Team _team;
    [Header("Stats Values")]
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private int _defenceValue;
    [SerializeField] private int _movementSpeedValue;

    public GameObject Prefab => _prefab;
    public int HealthValue => _healthValue;
    public int DamageValue => _damageValue;
    public int DefenceValue => _defenceValue;
    public int MovementSpeedValue => _movementSpeedValue;
    
    public Team Team => _team;
}