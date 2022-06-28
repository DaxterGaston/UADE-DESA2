using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DP.Prototype;
using UnityEngine;

public class KillAllWinCondiition : WinCondition
{
    private List<IObserver<LevelState>> _subscribers = new List<IObserver<LevelState>>();
    private bool _winConditionMet;

    [SerializeField] private List<BaseEnemy> _enemies; 
    
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
        if (_enemies != null && _enemies.All((enemy) => enemy.IsDead))
        {
            _winConditionMet = true;
            NotifyAll(LevelState.WinConditionMet);    
        }
    }

    private void Update()
    {
        CheckWinCondition();
    }
}