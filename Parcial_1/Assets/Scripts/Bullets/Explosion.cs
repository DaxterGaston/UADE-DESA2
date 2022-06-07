using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

namespace Assets.Scripts.Bullets
{
    public class Explosion : MonoBehaviour
    {
        private Stopwatch _sw;
        private TimeSpan _ts;
        // Use this for initialization
        void Start()
        {
            _sw = new Stopwatch();
            _ts = new TimeSpan(0,0,0,0, 500);
            _sw.Start();
            // GetComponent<Animator>().Play("Explosion");
        }

        // Update is called once per frame
        void Update()
        {
            if (_sw.Elapsed > _ts) Destroy(gameObject);
        }
    }
}