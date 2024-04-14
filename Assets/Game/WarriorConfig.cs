using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WarriorConfig", menuName = "ScriptableObject/Configs/WarriorConfig")]
public sealed class WarriorConfig : ScriptableObject
{
    [FormerlySerializedAs("_teams")] [SerializeField] private Team team;
    
    // TODO - можно сделать int(можно взять даже тип поменьше, если нам не нужны космические значения)
    // кастомной структорой с валидацией, т.к наши значения с высокой долей вероятности не будут уходить в минус
    //а также выделить отдельный клас для всех этих статов, но они будут разница от сущности к сущности
    [Header("Stats Values")]
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private int _defenceValue;
    [SerializeField] private int _movementSpeedValue;

    public int HealthValue => _healthValue;
    public int DamageValue => _damageValue;
    public int DefenceValue => _defenceValue;
    public int MovementSpeedValueValue => _movementSpeedValue;
    
    public IStats<int> Stats => _stats;
    public Team Team => team;
    
    private IStats<int> _stats;
}

public class MovementSpeed : ITypeStat<int>
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