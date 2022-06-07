using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Assets.Scripts.Abstractions;

namespace Assets.Scripts.Bullets
{
    public class BaseBullet : MonoBehaviour, IProduct<BaseBulletSO>
    {
        [SerializeField]
        protected BaseBulletSO _stats;
        protected IPool<BaseBullet> _pool;
        protected Vector3 _dir;
        protected Stopwatch _sw;
        protected TimeSpan _ts;

        protected float _extraSpeed;

        public BaseBulletSO Data => _stats;

        // Update is called once per frame
        public virtual void Update()
        {
            transform.position += _dir * (Time.deltaTime * (_stats.speed + _extraSpeed));
            if(_sw.Elapsed >= _ts) _pool.Store(this);
        }

        public virtual void SetBullet(float xDir, IPool<BaseBullet> pool, Transform firePoint, float extraSpeed = 0)
        {
            _extraSpeed = extraSpeed;
            transform.position = firePoint.position;
            _dir = new Vector3(xDir, 0, 0);
            _pool = pool;
            _ts = new TimeSpan(0, 0, _stats.lifeInSeconds);
            _sw = new Stopwatch();
            _sw.Start();
        }

        public void Store()
        {
            _pool.Store(this);
        }
    }
}