using System;
using UnityEngine;

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