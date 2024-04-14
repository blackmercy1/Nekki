using Ranges;
using UnityEngine;

//Main объект данного проекта, он являеется корневым и самым высоким в цепочки иерархии 
public class GameCoreController : MonoBehaviour
{
    [SerializeField] private WarriorInstaller _warriorInstaller;
    [SerializeField] private GameUpdates _gameUpdates;
    
    private void Awake()
    {
        var timer = CreateTimer();
        _gameUpdates.AddToUpdateList(timer);
        _gameUpdates.ResumeUpdate();
        _warriorInstaller.Initialize(timer);
    }
    
    // не хочу тратить много времени на// создание и конфигурацию десятков конфигов, поэтому
    // используются магические числа
    private RandomLoopTimer CreateTimer()
    {
        return new RandomLoopTimer(new FloatRange(1, 3)); 
    }
}