using UnityEngine;

public interface IFactory<T, S> where T : IProduct<S> where S : ScriptableObject
{
    T Product { get; }

    T Create();
    T[] Create(int quantity);
}
