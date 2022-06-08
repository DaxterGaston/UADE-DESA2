using UnityEngine;
using System.Collections;
using Assets.Scripts.Abstractions;

namespace Assets.Scripts.Bullets
{
    public class Bomb : ExplosiveBullet
    {
        [SerializeField]
        [Range(1, 10)]
        private float _force;
        private Rigidbody2D _rb;

        public override void SetBullet(float xDir, IPool<BaseBullet> pool, Transform firepoint, float extraSpeed = 0)
        {
            _rb = GetComponent<Rigidbody2D>();
            base.SetBullet(xDir, pool, firepoint);
            _rb.AddForce(new Vector2(xDir, _force) * _stats.speed);
        }

        public override void Update()
        {
            
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            print("puto viejo");
        }
    }
}