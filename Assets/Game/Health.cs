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