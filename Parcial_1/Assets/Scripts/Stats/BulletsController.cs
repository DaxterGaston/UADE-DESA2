using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Abstractions;

public class BulletsController : MonoBehaviour, IObservable
{
    public List<IObserver> Subscribers => _subscribers;
    private List<IObserver> _subscribers = new List<IObserver>();

    public void Subscribe(IObserver observer)
    {
        if (_subscribers.Contains(observer)) return;

        _subscribers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        if (!_subscribers.Contains(observer)) return;

        _subscribers.Remove(observer);
    }

    public void NotifyAll(string message, params object[] args)
    {
        foreach (var subscriber in _subscribers)
            subscriber.OnNotify(message, args);
    }

    
}
