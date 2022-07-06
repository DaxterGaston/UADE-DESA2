﻿using System;
using UnityEngine;
using Assets.Scripts.DP.Commands;
using Assets.Scripts.DP.Factory;
using Assets.Scripts.Abstractions;
using Assets.Scripts.Bullets;
using TMPro;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player
{
    public delegate void PlayerEventHandler();
    public class PlayerController : MonoBehaviour
    {
        #region SetUp

        #region Serializables

        [SerializeField]
        private BaseBullet _bulletPrefab;

        [SerializeField]
        private Transform _shootPoint;

        [SerializeField]
        private int _maxAmmo;

        [SerializeField]
        private float _movementSpeed;

        [SerializeField]
        private float _jumpForce;

        [SerializeField] 
        private Animator _damageOverlayAnimator;

        [SerializeField] private Transform _feetTopLeft;
        [SerializeField] private Transform _feetBottomRight;
        [SerializeField] private LayerMask _groundLayers;
        [SerializeField] private AudioSource shootAudioSource;
        [SerializeField] private AudioSource _walkingSound;
        [Range(0,10)][SerializeField] private float _extraGravity;
        #endregion

        private IFactory<BaseBullet, BaseBulletSO> _bulletsFactory;
        private readonly int START = Animator.StringToHash("Start");
        private Rigidbody2D _rigidbody;
        public event PlayerEventHandler OnPlayerDie;
        public bool Grounded { get; private set; }
        public bool Left { get; private set; }

        #region Observer

        private IPool<BaseBullet> _bullets;
        private HealthController _playerHealth;
        private GUIController _GUIObserver;
        private bool _dead;

        #endregion

        #region Commands

        private MoveCommand _moveLeft;
        private MoveCommand _moveRight;
        private JumpCommand _jump;
        private ShootCommand _shootLeft;
        private ShootCommand _shootRight;

        #endregion

        // Use this for initialization
        private void Start()
        {
            _playerHealth = GetComponent<HealthController>();
            _dead = false;
            _rigidbody = GetComponent<Rigidbody2D>();
            
            
            //Move
            _moveLeft = new MoveCommand(transform, new Vector3(-1, 0), _movementSpeed);
            _moveRight = new MoveCommand(transform, new Vector3(1, 0), _movementSpeed);
            _jump = new JumpCommand(_rigidbody, _jumpForce);
            
            //Attack
            _bulletsFactory = new BulletFactory(_bulletPrefab);
            _bullets = new BulletPool<BaseBullet, BaseBulletSO>(_bulletsFactory, _maxAmmo);
            _shootLeft = new ShootCommand(_shootPoint, -1f, _bullets);
            _shootRight = new ShootCommand(_shootPoint, 1f, _bullets);

            //Observer
            _GUIObserver = GetComponent<GUIController>();
            _playerHealth.Subscribe(_GUIObserver);
            ((BulletPool<BaseBullet, BaseBulletSO>)_bullets).Subscribe(_GUIObserver);
        }

        #endregion

        // Update is called once per frame
        private void Update()
        {
            if (_playerHealth.Health <= 0 && !_dead) Die();
            if (transform.position.y < -30 && !_dead) Die();
            ManageImputs();
            _rigidbody.AddForce(Vector2.down * _extraGravity * Time.deltaTime);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {

            if (collision.gameObject.CompareTag("Ground"))
            {
                Grounded = Physics2D.OverlapArea(_feetTopLeft.position, _feetBottomRight.position, _groundLayers);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("EnemyBullet"))
            {
                _playerHealth.Damage(1);
                CameraEffects.ShakeOnce(0.5f, 5, new Vector3(0.5f, 0.5f, 0));
                _damageOverlayAnimator.SetTrigger(START);
                var bullet = other.GetComponent<BaseBullet>();
                if (bullet != null) bullet.Store();
            } 
            if (other.gameObject.CompareTag("VictoryZone"))
            {
                print("AAAA");
                transform.position = new Vector3(-36, -11);
            }  
        }

        private void Die()
        {
            _dead = true;
            OnPlayerDie?.Invoke();
        }

        private void ManageImputs()
        {
            if (Input.GetKey(KeyCode.A))
            { 
                _moveLeft.Execute();
                if (!_walkingSound.isPlaying && Grounded) _walkingSound.Play();
                Left = true;
            }
            if (Input.GetKey(KeyCode.D)) 
            {
                _moveRight.Execute();
                if (!_walkingSound.isPlaying && Grounded) _walkingSound.Play();
                Left = false;
            }
            if (Input.GetKey(KeyCode.Space) && Grounded)
            {
                _jump.Execute();
                Grounded = false;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                shootAudioSource.Play();
                if (Left) _shootLeft.Execute();
                else _shootRight.Execute();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                _damageOverlayAnimator.SetTrigger(START);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawLine(_feetBottomRight.position, _feetTopLeft.position);
        }
    }
}