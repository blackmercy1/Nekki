using System;

public class Health : ITypeStat<int>
{
    public event Action Died ;
    
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
        
        if (IsDead())
            Died?.Invoke();
    }

    private bool IsDead()
    {
        return _value <= 0;
    }

    public string Id()
    {
        return _id;
    }
}

public class Died
{
}