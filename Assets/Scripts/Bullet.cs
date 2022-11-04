using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Bullet : MonoBehaviour
{
    
    private float _speed = 5f;
    private Vector3 _target = Vector3.zero;
    private int _damage;
    public void Init(int damage, Vector3 target)
    {
        _damage = damage;
        _target = target;
        Destroy(gameObject, 5);// replace collision 
    }
    private void Update() => transform.Translate(_speed * Time.deltaTime * _target, Space.World);
}
