using Assets.Scripts.Abstractions;
using Assets.Scripts.Bullets;
using UnityEngine;

public class ShootCommand : ICommand
{
    private IPool<BaseBullet> _bullets;
    private Transform _shootPoint;
    private float _xDir;
    private float _bulletSpeed;

    public ShootCommand(Transform shootPoint, float xDir, IPool<BaseBullet> bullets, float bulletSpeed = 3f)
    {
        _shootPoint = shootPoint;
        _xDir = xDir;
        _bulletSpeed = bulletSpeed;
        _bullets = bullets;
    }

    public void Execute()
    {
        if(_bullets.IsAvailable > 0)
        {
            _bullets.GetInstance().GetComponent<BaseBullet>().SetBullet(_xDir, _bullets, _shootPoint);;
        }
    }
}
