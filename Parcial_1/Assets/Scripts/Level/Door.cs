using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IObservable<LevelState>
{
    [SerializeField] private GameObject _closedSprite;
    [SerializeField] private GameObject _openSprite;

    
    private bool _open;
    public bool IsOpen => _open;
    private List<IObserver<LevelState>> _subscribers = new List<IObserver<LevelState>>(); 
    
    public List<IObserver<LevelState>> Subscribers { get; }

    private void Start()
    {
        _closedSprite.SetActive(true);
        _openSprite.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && IsOpen)
        {
            NotifyAll(LevelState.EnteredDoor);
        }
    }

    public void OpenDoor()
    {
        _open = true;
        _closedSprite.SetActive(false);
        _openSprite.SetActive(true);
    } 
    
    public void Subscribe(IObserver<LevelState> observer)
    {
        if (_subscribers.Contains(observer)) return;
        _subscribers.Add(observer);
    }

    public void Unsubscribe(IObserver<LevelState> observer)
    {
        if(!_subscribers.Contains(observer)) return;
        _subscribers.Remove(observer);
    }

    public void NotifyAll(LevelState message, params object[] args)
    {
        _subscribers?.ForEach((observer) => observer.OnNotify(message));
    }
}
