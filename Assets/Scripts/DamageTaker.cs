using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    [SerializeField] private AudioClip _deathSound;
    private GameController _gameController;

    public void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }
    
    public void TakeDamage(int damageValue)
    {
        if (damageValue > _health)
        {
            _health = 0;
        }
        else
        {
            _health -= damageValue;
        }
        if (_health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_deathSound)
        {
            AudioSource.PlayClipAtPoint(_deathSound, Camera.main.transform.position);
        }

        Destroy(gameObject);

        if (gameObject.name == "Player")
        {
            _gameController.EndGame();
        }
        else
        {
            _gameController.IncreaseKillCount();
        }
    }
}
