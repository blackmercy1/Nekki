using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WizardConfig", menuName = "ScriptableObject/Configs/WizardConfig")]
public sealed class WizardConfig : ScriptableObject
{
    [SerializeField] private Team _team;
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private int _defenceValue;
    
    public IStats<int> Stats => _stats;
    public Team Team => _team;
    
    
}