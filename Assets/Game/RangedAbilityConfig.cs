using UnityEngine;

[CreateAssetMenu(fileName = "RangedAbilityConfig", menuName = "ScriptableObject/Configs/Abilities/RangedAbility")]
public class RangedAbilityConfig : ScriptableObject
{
    [SerializeField] private RangedAbility _rangedAbilityPrefab;
    [SerializeField] private int _flySpeed;

    public RangedAbility RangedAbility => _rangedAbilityPrefab;
    public int FlySpeed => _flySpeed;
}