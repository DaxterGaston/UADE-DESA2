using Assets.Scripts.Abstractions;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DP.Prototype
{
    public class GreenEnemy : MonoBehaviour, IPrototype
    {
        public GameObject Clone()
        {
            GameObject clone = Instantiate(gameObject);
            clone.SetActive(false);
            return clone;
        }
    }
}