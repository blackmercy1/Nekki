using UnityEngine;

[CreateAssetMenu(fileName = "WizardConfig", menuName = "ScriptableObject/Configs/WizardConfig")]
public sealed class WizardConfig : ScriptableObject
{
    [SerializeField] private CollisionComponent _collisionComponent;
    [SerializeField] private MemberTeams _teams;
    
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private int _defenceValue;
    
    public IStats<int> Stats => _stats;
    public MemberTeams Teams => _teams;
    public CollisionComponent CollisionComponent => _collisionComponent;

    private IStats<int> _stats;
    
    private void Awake()
    {
        _stats = new Stats();
        _stats
            .Add(new Health("health", _healthValue))
            .Add(new Damage("damage", _damageValue))
            .Add(new Defence("defence", _defenceValue));
    }
}