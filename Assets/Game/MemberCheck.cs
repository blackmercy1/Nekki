using UnityEngine;

public class MemberCheck : ICheck
{
    public bool Check(GameObject gameObject)
    {
        return gameObject.TryGetComponent<IMember>(out _);
    }
}