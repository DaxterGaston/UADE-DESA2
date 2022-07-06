using System.Collections.Generic;
using UnityEngine;

public class ArenaWinCondition: WinCondition
{
    private List<IObserver<LevelState>> _subscribers = new List<IObserver<LevelState>>();
    private bool _winConditionMet;
    private int _killCount;
    
    [SerializeField] private int _winScore;
    [SerializeField] private EnemiesController _enemiesController;

    public int WinScore => _winScore;
    public int KillCount => _killCount;
    
    private void Start()
    {
        _enemiesController.OnEnemyKilled += () =>
        {
            _killCount++;
            CheckWinCondition();
        };
        
    }

    public List<IObserver<LevelState>> Subscribers => _subscribers;
    public bool WinConditionMet => _winConditionMet;

    public override void Subscribe(IObserver<LevelState> observer)
    {
        if (_subscribers.Contains(observer)) return;
        _subscribers.Add(observer);
    }

    public override void Unsubscribe(IObserver<LevelState> observer)
    {
        if (!_subscribers.Contains(observer)) return;
        _subscribers.Remove(observer);
    }

    public override void NotifyAll(LevelState message, params object[] args)
    {
        if (_subscribers.Count == 0) return;
        _subscribers.ForEach((observer) => observer.OnNotify(message));
    }

    public override void CheckWinCondition()
    {
        if (_killCount >= _winScore)
        {
            _winConditionMet = true;
            _enemiesController.StopSpawning();
            _enemiesController.KillAllEnemies();
            NotifyAll(LevelState.WinConditionMet);
        }
    }
}