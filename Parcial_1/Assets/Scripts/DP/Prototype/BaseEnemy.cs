﻿using Assets.Scripts.Abstractions;
using Assets.Scripts.Bullets;
using Assets.Scripts.DP.Factory;
using System;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.DP.Prototype
{
    public class BaseEnemy : MonoBehaviour, IPrototype
    {
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private Transform _shootPoint;
        [SerializeField]
        private BaseBullet _bulletPrefab;
        [SerializeField]
        private float _canSeePlayerRange;
        [SerializeField]
        private float _canShootPlayerRange;
        [SerializeField]
        private LayerMask _player;

        private Vector3 _canSee;
        private Vector3 _canShoot;

        private Stopwatch _sw;
        private TimeSpan _ts;

        private bool _left;

        private bool _dead;
        
        MoveCommand _moveLeft;
        MoveCommand _moveRight;
        ShootCommand _shootLeft;
        ShootCommand _shootRight;

        private HealthController _health;


        private IFactory<BaseBullet, BaseBulletSO> _baseBulletFactory;
        private IPool<BaseBullet> _baseBulletPool;

        public bool IsDead => _dead;
        
        public GameObject Clone()
        {
            GameObject go = GameObject.Instantiate(gameObject);
            go.SetActive(false);
            return go;
        }

        // Use this for initialization
        void Start()
        {
            _moveLeft = new MoveCommand(transform, new Vector3(-1, 0), _movementSpeed);
            _moveRight = new MoveCommand(transform, new Vector3(1, 0), _movementSpeed);

            _baseBulletFactory = new BulletFactory(_bulletPrefab);
            _baseBulletPool = new BulletPool<BaseBullet, BaseBulletSO>(_baseBulletFactory, 5);
            _shootLeft = new ShootCommand(_shootPoint, -1f, _baseBulletPool, 1);
            _shootRight = new ShootCommand(_shootPoint, 1f, _baseBulletPool, 1);

            _health = GetComponent<HealthController>();

            _sw = new Stopwatch();
            _ts = new TimeSpan(0, 0, 2);
            _sw.Start();
        }

        // Update is called once per frame
        void Update()
        {
            if (_health.Health == 0)
            {
                gameObject.SetActive(false);
                _dead = true;
            }
            Vector3 _canSee = transform.position;
            _canSee.x += _canSeePlayerRange;
            RaycastHit2D seen = Physics2D.Raycast(transform.position, transform.right, _canSeePlayerRange, _player);
            if (seen)
            {
                if(seen.collider.transform.position.x > transform.position.x) _left = false;
                else _left = true;
                RaycastHit2D onShootRange = Physics2D.Raycast(transform.position, transform.right, _canShootPlayerRange, _player);
                if (onShootRange)
                {
                    if(_sw.Elapsed > _ts)
                    {
                    if (_left) _shootLeft.Execute();
                    else _shootRight.Execute();
                    _sw.Restart();
                    }
                }
                else
                {
                    if(_left) _moveLeft.Execute();
                    else _moveRight.Execute();
                }
            }
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawLine(transform.position, (Vector2)transform.position + (Vector2)transform.forward);
            Gizmos.color = Color.magenta;
            Vector3 _canSee = transform.position;
            _canSee.x += _canSeePlayerRange;
            Gizmos.DrawLine(transform.position, _canSee);

            Gizmos.color = Color.red;
            Vector3 _canShoot = transform.position;
            _canShoot.x += _canShootPlayerRange;
            Gizmos.DrawLine(transform.position, _canShoot);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            {
                BaseBullet bb = collision.gameObject.GetComponent<BaseBullet>();
                _health.Damage(bb.Data.damage);
                bb.Store();
            }
        }

    }
}