using UnityEngine;

public interface IProduct<T> where T : ScriptableObject
{
    T Data { get; }
}