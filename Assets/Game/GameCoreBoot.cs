using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCoreController : MonoBehaviour
{
   
}

public abstract class EntityFactory
{
    public abstract IEntity CreateEntity();
}

public sealed class WizardEntityFactory : EntityFactory
{
    public override IEntity CreateEntity()
    {
        return null;
        // return new Wizard();
    }
}

public sealed class WizardInstaller : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private WizardConfig _wizardConfig;
}

[CreateAssetMenu(fileName = "WizardConfig", menuName = "ScriptableObject/Configs/WizardConfig")]
public sealed class WizardConfig : ScriptableObject
{
    [SerializeField] private MemberTeams _teams;
    
    public Stats Stats => _stats;
    public MemberTeams Teams => _teams;
    
    private Stats _stats;
    
    private void Awake()
    {   
        _stats
            .Add(new Health())
            .Add(new Damage())
            .Add(new Defence());
    }
}


//Две разные фабрики, потому что в реальности сщуности могут отличаться и иметь различный набор параметро
public sealed class WarriorEntityCreator : EntityFactory
{
    public WarriorEntityCreator()
    {
        
    }
    
    public override IEntity CreateEntity()
    {
        // return new Warrior();
        return null;
    }
}

public interface IEntity : IMember
{
    CollisionComponent CollisionComponent { get; }
    void Method();
}

public sealed class Wizard : IEntity, IDamageable
{
    public CollisionComponent CollisionComponent { get; }
    public MemberTeams Teams { get; }

    private Stats _stats;

    public Wizard(CollisionComponent collisionComponent, MemberTeams teams, Stats stats)
    {
        CollisionComponent = collisionComponent;
        Teams = teams;
        _stats = stats;
    }
    
    public void Method()
    {
        
    }

    public void TakeDamage(int damagePoints)
    {
        
    }
}

public sealed class Warrior : IEntity, IDamageable
{
    public CollisionComponent CollisionComponent { get; }
    public MemberTeams Teams { get;}
    
    private Stats _stats;

    public Warrior(MemberTeams teams, CollisionComponent collisionComponent, Stats stats)
    {
        Teams = teams;
        CollisionComponent = collisionComponent;
        _stats = stats;
    }
    
    public void Method()
    {
        
    }

    public void TakeDamage(int damagePoints)
    {
    }
}

public interface IDamageable
{
    void TakeDamage(int damagePoints);
}

public interface IMember
{
    MemberTeams Teams { get;}
}

public enum MemberTeams
{
    Red,
    Blue
}

public sealed class CollisionComponent : MonoBehaviour
{
    private Action<GameObject> _collisionReaction;

    public void Init(Action<GameObject> collisionReaction)
    {
        _collisionReaction = collisionReaction;
    }

    private void OnCollisionEnter(Collision collision) => _collisionReaction?.Invoke(collision.gameObject);
}

public sealed class AttackFilter
{
    private readonly ICheck _memberCheck;
    private readonly ICheck _damageableCheck;

    public AttackFilter(ICheck memberCheck, ICheck damageableCheck)
    {
        _memberCheck = memberCheck;
        _damageableCheck = damageableCheck;
    }
    
    public void AddAttackFilter(GameObject gameObject)
    {
        if (_memberCheck.Check(gameObject))
            if (_damageableCheck.Check(gameObject))
                Console.WriteLine();
    }
}

public class MemberCheck : ICheck
{
    public bool Check(GameObject gameObject)
    {
        return gameObject.TryGetComponent<IMember>(out _);
    }
}

public interface ICheck
{
    bool Check(GameObject gameObject);
}

public struct Health : ITypeStat<int>
{
    private readonly string _id;
    private int _value;
    
    public Health(string id, int value)
    {
        _id = id;
        _value = value;
    }
    
    public int GetValue()
    {
        return _value;
    }

    public void Add(int value)
    {
        _value += value;
    }

    public string Id()
    {
        return _id;
    }
}

public struct Defence : ITypeStat<int>
{
    private readonly string _id;
    private int _value;
    
    public Defence(int value, string id)
    {
        _value = value;
        _id = id;
    }

    public int GetValue() => _value;

    public void Add(int value)
    {
        _value += value;
    }

    public string Id()
    {
        return _id;
    }
}

public struct Damage : ITypeStat<int>
{
    private readonly string _id;
    private int _value;
    
    public Damage(int value, string id)
    {
        _id = id;
        _value = value;
    }

    public int GetValue() => _value;

    public void Add(int value)
    {
        _value += value;
    }

    public string Id()
    {
        return _id;
    }
}

public interface IStats<T>
{
    bool TryGet(string id, out ITypeStat<T> stat);
    IStats<T> Add(ITypeStat<T> stat);
}

public interface ITypeStat<T> : IStat<T>
{
    string Id();
}

public interface IStat<T>
{
    T GetValue();

    void Add(T value);
}

public class GenericStats<T> : IStats<T>
{
    private readonly Dictionary<string, ITypeStat<T>> _stats;

    public GenericStats() : this(new Dictionary<string, ITypeStat<T>>())
    {
    }

    private GenericStats(Dictionary<string, ITypeStat<T>> stats)
    {
        _stats = stats;
    }

    public bool TryGet(string id, out ITypeStat<T> stat)
    {
        return _stats.TryGetValue(id, out stat);
    }

    public IStats<T> Add(ITypeStat<T> stat)
    {
        _stats[stat.Id()] = stat;
        return this;
    }
}

public class Stats : IStats<int>
{
    private readonly GenericStats<int> _intStats;

    public Stats(GenericStats<int> intStats)
    {
        _intStats = intStats;
    }

    public bool TryGet(string id, out ITypeStat<int> stat)
    {
        return _intStats.TryGet(id, out stat);
    }

    public IStats<int> Add(ITypeStat<int> stat)
    {
        _intStats.Add(stat);
        return this;
    }
}

public abstract class StatsDecorator<T> : IStat<T>
{
    protected readonly IStat<T> Child;
    
    public StatsDecorator(IStat<T> child)
    {
        Child = child;
    }

    public abstract T GetValue();

    public abstract void Add(T value);
}

public class IntAdditionDecorator : StatsDecorator<int>
{
    private readonly IStat<int> _stat;

    public IntAdditionDecorator(IStat<int> stat,IStat<int> child) : base(child)
    {
        _stat = stat;
    }

    public override int GetValue()
    {
        return _stat.GetValue() + Child.GetValue();
    }

    public override void Add(int value)
    {
        Child.Add(value);
    }
}


