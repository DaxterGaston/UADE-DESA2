using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Bullet", order = 1)]
public class BaseBulletSO : ScriptableObject
{
    public float speed;
    public int damage;
    public int lifeInSeconds;
}