using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DP.Prototype;
using UnityEngine;
using Random = UnityEngine.Random;

public delegate void EnemiesControllerEventHandler();

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private List<BaseEnemy> _enemies;
    [SerializeField] private bool _enemySpawnLoop;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private BaseEnemy enemyPrefab;
    [SerializeField] private float _enemySpawnWaitTime;

    private float _enemySpawntimer;
    
    public event EnemiesControllerEventHandler OnAllEnemiesKilled;
    public event EnemiesControllerEventHandler OnEnemyKilled;

    private void Start()
    {
        if (_enemies.Count > 0)
        {
            _enemies.ForEach((enemy) => enemy.OnDead += EnemyKilled);
            _enemySpawntimer = _enemySpawnWaitTime;    
        }
    }

    private void Update()
    {
        if (!_enemySpawnLoop) return;

        if (_enemySpawntimer <= 0)
        {
            SpawnNewEnemy();
            _enemySpawntimer = _enemySpawnWaitTime;
        }
        _enemySpawntimer -= _enemySpawnWaitTime;


    }

    private void EnemyKilled()
    {
        OnEnemyKilled?.Invoke();
        if (_enemies != null && _enemies.All((enemy) => enemy.IsDead))
        {
            OnAllEnemiesKilled?.Invoke();
        }
    }

    private void SpawnNewEnemy()
    {
        if (_spawnPoints.Count > 0)
        {
            var index= Random.Range(0, _spawnPoints.Count);
            Instantiate(enemyPrefab, _spawnPoints[index].transform.position, _spawnPoints[index].transform.rotation);
        }
    }

    public void StopSpawning()
    {
        _enemySpawnLoop = false;
    }

    public void KillAllEnemies()
    {
        _enemies.ForEach((enemy) => enemy.Kill());
    }
}