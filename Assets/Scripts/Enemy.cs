using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Movement")] [SerializeField] [Range(1f, 2f)] private float _speed = 1f;
    [SerializeField] private AudioClip _snatchSound;
    
    private GameController _gameController;
    private DamageDealer _damageDealer;
    private List<int> _directions = new List<int>() { -1, 1 };
    private int _direction;
    
    // Start is called before the first frame update
    void Start()
    {
        _damageDealer = GetComponent<DamageDealer>();
        _gameController = FindObjectOfType<GameController>();
        _direction = _directions[Random.Range(0, _directions.Count)];

        if (_direction < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector2(_direction * -1, 0) * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Transform objectTransform = other.gameObject.transform;
        
        if (objectTransform.parent && objectTransform.parent.name == "Boundaries")
        {
            _direction = _direction * -1;
            bool isFlipped = _direction < 0;
            
            gameObject.GetComponent<SpriteRenderer>().flipX = isFlipped;
        }

        if (other.gameObject.name == "House")
        {
            AudioSource.PlayClipAtPoint(_snatchSound, Camera.main.transform.position);
            _gameController.DecreaseVegetableCount(_damageDealer.GetDamageValue());
            Destroy(gameObject);
        }
    }
}
