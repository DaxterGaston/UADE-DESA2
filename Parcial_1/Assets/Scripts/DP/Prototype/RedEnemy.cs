using Assets.Scripts.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DP.Prototype
{
    public class RedEnemy : MonoBehaviour, IPrototype
    {
        public GameObject Clone()
        {
            GameObject clone = Instantiate(gameObject);
            clone.SetActive(false);
            return clone;
        }
    }
}
