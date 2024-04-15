using UnityEngine;

public class Instantiator : MonoBehaviour
{
    private void Awake() => DontDestroyOnLoad(gameObject);
    
    public static GameObject InstantiateGameObject(GameObject prefab) => Instantiate(prefab);
}