using UnityEngine;

public abstract class EnityCreator<T>
{
    public GameObject Prefab;
    
    public abstract T CreateEntity();
}