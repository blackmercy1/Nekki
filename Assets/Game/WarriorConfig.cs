using UnityEngine;

[CreateAssetMenu(fileName = "WarriorConfig", menuName = "ScriptableObject/Configs/WarriorConfig")]
public sealed class WarriorConfig : ScriptableObject
{
    [SerializeField] private CollisionComponent _collisionComponent;
    [SerializeField] private MemberTeams _teams;
    
    // TODO - можно сделать int(можно взять даже тип поменьше, если нам не нужны космические значения)
    // кастомной структорой с валидацией, т.к наши значения с высокой долей вероятности не будут уходить в минус
    [Header("Stats Values")]
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private int _defenceValue;
    [SerializeField] private int _movementSpeed;

    public CollisionComponent CollisionComponent => _collisionComponent;
    public IStats<int> Stats => _stats;
    public MemberTeams Teams => _teams;
    
    private IStats<int> _stats;
    
    private void Awake()
    {
        _stats = new Stats()
            .Add(new Health("health", _healthValue))
            .Add(new Damage("damage", _damageValue))
            .Add(new Defence("defence", _defenceValue))
            .Add(new MovementSpeed("movementSpeed", _movementSpeed));
    }
}

public struct MovementSpeed : ITypeStat<int>
{
    private readonly string _id;
    private int _value;
    
    public MovementSpeed(string id, int value)
    {
        _id = id;
        _value = value;
    }

    public int GetValue() => _value;

    public void Add(int value)
    {
        _value += value;
    }

    public string Id() => _id;
}