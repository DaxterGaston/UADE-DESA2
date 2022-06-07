using Assets.Scripts.Abstractions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Transform _transform;
    private Vector3 _direction;
    private float _speed;

    public MoveCommand(Transform transform, Vector3 direction, float speed)
    {
        _transform = transform;
        _direction = direction;
        _speed = speed;
    }

    public void Execute()
    {
        _transform.position += _direction * Time.deltaTime * _speed;
    }
}
