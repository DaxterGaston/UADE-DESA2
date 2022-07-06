using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Abstractions;


namespace Assets.Scripts.DP.Factory
{
    public class BulletPool<T, S> : IObservable, IPool<T> where T : MonoBehaviour, IProduct<S> where S : ScriptableObject
    {
        private const string OBSERVER_MESSAGE = "AMMO_CHANGE";
        private List<T> inUse = new List<T>();
        private List<T> _used = new List<T>();
        private List<T> available = new List<T>();
        private int _maxAmmount;
        public int IsAvailable => available.Count;
        public int MaxAmmount => _maxAmmount;

        public List<IObserver> Subscribers => _subscribers;
        private List<IObserver> _subscribers = new List<IObserver>();

        public BulletPool(IFactory<T, S> factory, int maxAmmount)
        {
            T[] bullets = factory.Create(maxAmmount);
            _maxAmmount = maxAmmount;
            foreach (var item in bullets)
            {
                Store(item);
            }
        }

        public T GetInstance()
        {
            if (IsAvailable > 0)
            {
                T temp = available[0];
                available.Remove(temp);
                inUse.Add(temp);
                //temp.enabled = true;
                temp.gameObject.SetActive(true);
                NotifyAll(OBSERVER_MESSAGE, (IsAvailable + "/" + _maxAmmount));
                return temp;
            }
            return default(T);
        }

        public void Store(T item)
        {
            available.Add(item);
            item.gameObject.SetActive(false);
            //item.enabled = false;
            if (inUse.Contains(item)) //Si esta en la lista...
                inUse.Remove(item); //Removelo
            NotifyAll(OBSERVER_MESSAGE, (IsAvailable + "/" + _maxAmmount));
        }

        public List<T> GetInUseItems()
        {
            List<T> list = new List<T>();
            list.AddRange(inUse);
            return list;
        }

        public void Subscribe(IObserver observer)
        {
            if (_subscribers.Contains(observer)) return;
            _subscribers.Add(observer);
            NotifyAll(OBSERVER_MESSAGE, (IsAvailable + "/" + _maxAmmount));
        }

        public void Unsubscribe(IObserver observer)
        {
            if (!_subscribers.Contains(observer)) return;
            _subscribers.Remove(observer);
        }

        public void NotifyAll(string message, params object[] args)
        {
            foreach (var subscriber in _subscribers)
                subscriber?.OnNotify(message, args);
        }

        public void StoreAsUsed(T item)
        {
            _used.Add(item);
            item.gameObject.SetActive(false);
            //item.enabled = false;
            if (inUse.Contains(item)) //Si esta en la lista...
                inUse.Remove(item); //Removelo
            NotifyAll(OBSERVER_MESSAGE, (IsAvailable + "/" + _maxAmmount));
        }

        public void SetAllUsedAsAvailable()
        { 
            
        }
    }
}