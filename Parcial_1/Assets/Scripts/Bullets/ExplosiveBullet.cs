using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Bullets;
using UnityEngine;

public class ExplosiveBullet : BaseBullet
{
    [SerializeField]
    private Transform _spriteTransform;
    private float _flipZ = -180;
    // Start is called before the first frame update
    void Start()
    {

        if(_dir.x < 0)
            _spriteTransform.Rotate(new Vector3(0, 0, _flipZ));
    }

    // Update is called once per frame
    public override void Update()
    {
        transform.position += _dir * (Time.deltaTime * _stats.speed);
        if(_sw.Elapsed >= _ts) Blow();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.tag);
        Blow();
    } 

    public virtual void OnTriggerEnter2D(Collider2D other) 
    {
        print(other.gameObject.tag);
        Blow();
    } 

    private void Blow()
    {
        ExplosiveBulletSO _data = (ExplosiveBulletSO)Data;
        Instantiate(_data.ExplosionPrefab, transform.position, Quaternion.identity);
        _pool.Store(this);
    }

}
