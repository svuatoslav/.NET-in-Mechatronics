using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bullet = null;
    [SerializeField] private Transform _bulletStartPosition = null;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Vector3 _target = Vector3.zero;
    [SerializeField] private int _hp = 100;

    private bool _fire = false;
    private int _damage = 4; // raplece weapon
    private Vector3 _direction = Vector3.zero;
    private bool _jump = false;
    private Rigidbody _rb = null;

    public int HP { get { return _hp; } set { _hp += value; } }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _fire = true;
        _direction.z = Input.GetAxis("Vertical");
        _direction.x = Input.GetAxis("Horizontal");//jump
    }
    private void FixedUpdate()
    {
        if(_fire)
            Fire();
        Move();
    }
    private void Move()
    {
        var speed = _direction * Time.fixedDeltaTime * _speed;
        transform.Translate(speed);
        
    }
    private void Fire()
    {
        var target = (_bulletStartPosition.position - transform.position) * 10;
        var bullet = Instantiate(_bullet, _bulletStartPosition.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Init(_damage, target);
        _fire = false;
    }
    private void PlayerDestroy()
    {
        if(_hp <= 0)
            Destroy(gameObject);
    }

}
