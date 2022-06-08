using Assets.Scripts.Abstractions;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DP.Prototype
{
    public class BlueEnemy : MonoBehaviour, IPrototype
    {
        public GameObject Clone()
        {
            GameObject clone = Instantiate(gameObject);
            clone.SetActive(false);
            return clone;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}