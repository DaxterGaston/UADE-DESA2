using Assets.Scripts.DP.Prototype;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private BaseEnemy _enemy;

        [SerializeField]
        private List<Transform> _spawnPoints;
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