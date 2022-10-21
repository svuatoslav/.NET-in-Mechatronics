using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private protected GameObject _player = null;
    private protected int _damage;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

}
