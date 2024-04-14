using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WizardConfig", menuName = "ScriptableObject/Configs/WizardConfig")]
public sealed class WizardConfig : ScriptableObject
{
    [SerializeField] private CollisionComponent _collisionComponent;
    [FormerlySerializedAs("_teams")] [SerializeField] private Team team;
    
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private int _defenceValue;
    
    public IStats<int> Stats => _stats;
    public Team Team => team;
    public CollisionComponent CollisionComponent => _collisionComponent;

    private IStats<int> _stats;
    
    private void Awake()
    {
    }
}