using Assets.Scripts.Abstractions;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, IHealth, IObservable
{
    [SerializeField]
    [Range(1, 50)]
    private int _maxHealth;

    private const string MESSAGE = "HEALTH_CHANGE";

    public int Health { get; private set; }
    
    public int MaxHealth => _maxHealth;

    public List<IObserver> Subscribers => _subscribers;
    private List<IObserver> _subscribers = new List<IObserver>();

    void Awake()
    {
        Health = _maxHealth;    
    }

    #region Metodos Publicos

    public void Heal(int amount)
    {
        Health += amount;
        NotifyAll(MESSAGE, (Health + "/" + _maxHealth));
    }

    public void Damage(int amount)
    {
        Health -= amount;
        //if (Health == 0) Destroy(gameObject);
        NotifyAll(MESSAGE, (Health + "/" + _maxHealth));
    }

    public void Subscribe(IObserver observer)
    {
        if (_subscribers.Contains(observer)) return;
        _subscribers.Add(observer);
        NotifyAll(MESSAGE, (Health + "/" + _maxHealth));
    }

    public void Unsubscribe(IObserver observer)
    {
        if (!_subscribers.Contains(observer)) return;

        _subscribers.Remove(observer);
    }

    public void NotifyAll(string message, params object[] args)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber?.OnNotify(message, args);
        }
    }

    #endregion
}
