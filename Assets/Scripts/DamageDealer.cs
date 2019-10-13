using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 5;

    private bool _isPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Player")
        {
            _isPlayer = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherObject = other.gameObject;
        if (otherObject.name == "KillBox")
        {
            var damageTaker = otherObject.GetComponentInParent<DamageTaker>();

            if (damageTaker)
            {
                damageTaker.TakeDamage(_damageAmount);
            }
        }
    }

    public int GetDamageValue()
    {
        return _damageAmount;
    }
}
