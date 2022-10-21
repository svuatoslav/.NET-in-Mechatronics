using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Enemy
{
    private void Start()
    {
        _damage = 25;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_player.tag))
        {
            other.gameObject.GetComponent<Player>().HP = -_damage;
            Destroy(gameObject);
        }    
    }
}
