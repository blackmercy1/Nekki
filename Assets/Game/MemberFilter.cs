using UnityEngine;

public class MemberFilter : FilterDecorator
{
    public MemberFilter() : base()
    {
        
    }
    
    public MemberFilter(IFilter child) : base(child)
    {
    }
    
    protected override bool CheckInternal(GameObject obj)
    {
        return Child != null && obj.TryGetComponent<IMember>(out _) && Child.Check(obj);
    }
}

public interface IMember
{
}