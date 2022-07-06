using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Abstractions;
using Assets.Scripts.Bullets;
using Assets.Scripts.DP.Factory;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System;

public class Boss : MonoBehaviour
{
    [SerializeField] ExplosiveBullet _bomb;
    [SerializeField] ExplosiveBullet _rocket;
    [SerializeField] Transform _firePoint;
    [SerializeField] LayerMask _player;

    MoveCommand _moveLeft;
    MoveCommand _moveRight;

    ShootCommand _shootRocketLeft;
    ShootCommand _shootRocketRight;
    ShootCommand _shootBombLeft;
    ShootCommand _shootBombRight;

    IFactory<BaseBullet, BaseBulletSO> _bombFactory;
    IFactory<BaseBullet, BaseBulletSO> _rocketFactory;
    IPool<BaseBullet> _bombPool;
    IPool<BaseBullet> _rocketPool;

    private bool _lastShootRocket = false;
    private bool _left = false;

    private Vector3 _rotateLeft = new Vector3(-2, 2, 2);
    private Vector3 _rotateRight = new Vector3(2, 2, 2);

    private Stopwatch _sw;
    private TimeSpan _ts;

    HealthController _health;

    // Start is called before the first frame update
    void Start()
    {
        _moveLeft = new MoveCommand(transform, Vector2.left, 2);
        _moveRight = new MoveCommand(transform, Vector2.right, 2);

        _bombFactory = new BulletFactory(_bomb);
        _rocketFactory = new BulletFactory(_rocket);
        _bombPool = new BulletPool<BaseBullet, BaseBulletSO>(_bombFactory, 10);
        _rocketPool = new BulletPool<BaseBullet, BaseBulletSO>(_rocketFactory, 10);

        _shootBombLeft = new ShootCommand(_firePoint, -1, _bombPool);
        _shootBombRight = new ShootCommand(_firePoint, 1f, _bombPool);

        _shootRocketLeft = new ShootCommand(_firePoint, -1, _rocketPool, 2);
        _shootRocketRight = new ShootCommand(_firePoint, 1f, _rocketPool, 2);

        _health = GetComponent<HealthController>();

        _sw = new Stopwatch();
        _ts = new TimeSpan(0,0,0,0,300);
        _sw.Start();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            BaseBullet bb = other.gameObject.GetComponent<BaseBullet>();
            _health.Damage(bb.Data.damage);
            bb.Store();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_health.Health == 0) SceneManager.LoadScene("SampleScene");
        RaycastHit2D _leftRay = Physics2D.Raycast(transform.position, Vector2.left, 10, _player);
        if(_leftRay)
        {
            _left = true;
            _moveLeft.Execute();
            UpdateRotation();
            Shoot();
        }
        RaycastHit2D _rightRay = Physics2D.Raycast(transform.position, Vector2.right, 10, _player);
        if (_rightRay)
        {
            _left = false;
            _moveRight.Execute();
            UpdateRotation();
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_sw.Elapsed >= _ts)
        {
            if (_left)
            {
                if (_lastShootRocket) _shootBombLeft.Execute();
                else _shootRocketLeft.Execute();
            }
            else
            {
                if (_lastShootRocket) _shootBombRight.Execute();
                else _shootRocketRight.Execute();
            }
            _sw.Restart();
        }
    }
    private void UpdateRotation()
    {
        if (_left)
        {
            transform.localScale = _rotateLeft; 
        }
        else
        {
            transform.localScale = _rotateRight;
        }
    }
}
