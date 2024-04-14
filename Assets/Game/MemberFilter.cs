using UnityEngine;

public class MemberFilter : FilterDecorator
{
    public MemberFilter(IFilter child) : base(child)
    {
    }
    
    protected override bool CheckInternal(GameObject obj)
    {
        return obj.TryGetComponent<IMember>(out _);
    }
}