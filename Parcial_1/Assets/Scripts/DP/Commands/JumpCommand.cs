using Assets.Scripts.Abstractions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : ICommand
{
    private Rigidbody2D _rb;
    private float _jumpForce;
    private Vector2 _clampedVelocity;

    public JumpCommand(Rigidbody2D rb, float jumpForce)
    {
        _rb = rb;
        _jumpForce = jumpForce;
        _clampedVelocity = new Vector2();
    }

    public void Execute()
    {
        _clampedVelocity = Vector2.up * _jumpForce;
        if (_clampedVelocity.y > 8) _clampedVelocity.y = 8;
        _rb.AddForce(_clampedVelocity);
    }
}
