using System;
using System.Collections.Generic;
using UnityEngine;

//Main объект данного проекта, он являеется корневым и самым высоким в цепочки иерархии 
public class GameCoreController : MonoBehaviour
{
   
}

public abstract class EntityFactory
{
    public abstract IEntity CreateEntity();
}

public sealed class WizardEntityCreator : EntityFactory
{
    private readonly WizardConfig _wizardConfig;

    public WizardEntityCreator(WizardConfig wizardConfig)
    {
        _wizardConfig = wizardConfig;
    }
    
    public override IEntity CreateEntity()
    {
        return new Wizard(
            _wizardConfig.Teams,
            _wizardConfig.CollisionComponent,
            _wizardConfig.Stats);
    }
}

[CreateAssetMenu(fileName = "WizardConfig", menuName = "ScriptableObject/Configs/WizardConfig")]
public sealed class WizardConfig : ScriptableObject
{
    [SerializeField] private MemberTeams _teams;
    [SerializeField] private CollisionComponent _collisionComponent;
    
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private int _defenceValue;
    
    public Stats Stats => _stats;
    public MemberTeams Teams => _teams;
    public CollisionComponent CollisionComponent => _collisionComponent;

    private readonly Stats _stats;
    
    private void Awake()
    {   
        _stats
            .Add(new Health("health", _healthValue))
            .Add(new Damage("damage", _damageValue))
            .Add(new Defence("defence", _defenceValue));
    }
}

public sealed class WizardInstaller : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private WizardConfig _wizardConfig;
}

public sealed class WarriorInstaller : MonoBehaviour
{
    public GameObject Prefab => _prefab;
    public WarriorConfig WizardConfig => _wizardConfig;
    
    [SerializeField] private GameObject _prefab;
    [SerializeField] private WarriorConfig _wizardConfig;

    private void Awake()
    {
        
    }
}

[CreateAssetMenu(fileName = "WizardConfig", menuName = "ScriptableObject/Configs/WarriorConfig")]
public sealed class WarriorConfig : ScriptableObject
{
    [SerializeField] private CollisionComponent _collisionComponent;
    [SerializeField] private MemberTeams _teams;
    
    // TODO - можно сделать int(можно взять даже тип поменьше, если нам не нужны космические значения)
    // кастомной структорой с валидацией, т.к наши значения с высокой долей вероятности не будут уходить в минус
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private int _defenceValue;

    public CollisionComponent CollisionComponent => _collisionComponent;
    public Stats Stats => _stats;
    public MemberTeams Teams => _teams;
    
    private Stats _stats;
    
    private void Awake()
    {   
        _stats
            .Add(new Health("health", _healthValue))
            .Add(new Damage("damage", _damageValue))
            .Add(new Defence("defence", _defenceValue));
    }
}

//Два разных фабричных метода (хотя в данном случае их можно объединить), потому что в реальности сщуности могут отличаться и иметь различный набор параметро
public sealed class WarriorEntityCreator : EntityFactory
{
    private readonly WarriorConfig _config;
    
    public WarriorEntityCreator(WarriorConfig config)
    {
        _config = config;
    }

    public override IEntity CreateEntity()
    {
        return new Warrior(
            _config.Teams,
            _config.CollisionComponent,
            _config.Stats);
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

    public Wizard(MemberTeams teams, CollisionComponent collisionComponent, Stats stats)
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

[Serializable]
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
    
    public Defence(string id, int value)
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
    
    public Damage(string id, int value)
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
    //в качестве id была выбрана string, да она медлеенее, но зато дебажить ее намного проще
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

//Класс для работы со статами, в данном случае int но при желании это легко можно поменять или дополнить
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


