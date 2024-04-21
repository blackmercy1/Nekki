using UnityEngine;

[CreateAssetMenu(fileName = "FireAbilityConfig", menuName = "ScriptableObject/Configs/Abilities/FireAbility")]
public class FireAbilityConfig : ScriptableObject
{
    [SerializeField] private FireAbility _rangedAbilityPrefab;
    [SerializeField] private int _flySpeed;

    public FireAbility RangedAbility => _rangedAbilityPrefab;
    public int FlySpeed => _flySpeed;
}