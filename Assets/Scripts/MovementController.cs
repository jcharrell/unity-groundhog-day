using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _jumpVelocity = 5f;
    
    private Animator _animator;
    private Camera _mainCamera;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private bool _jumpInProgress = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Ground Tilemap")
        {
            _jumpInProgress = false;
            _animator.SetBool("isJumping", _jumpInProgress);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var xDirection = Input.GetAxis("Horizontal");
        var jumpDirection = Input.GetAxis("Jump");
        if (xDirection < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }

        var walkingDirection = Input.GetAxis("Horizontal");
        _animator.SetBool("isWalking", walkingDirection != 0);
        
        gameObject.transform.Translate(new Vector2(walkingDirection, 0) * _moveSpeed * Time.deltaTime);
        _mainCamera.transform.position = new Vector3(gameObject.transform.position.x, _mainCamera.transform.position.y, _mainCamera.transform.position.z);

        if (jumpDirection > 0 && !_jumpInProgress)
        {
            _jumpInProgress = true;
            _animator.SetBool("isJumping", _jumpInProgress);
            _rigidbody2D.velocity = new Vector2(0, _jumpVelocity);
        }
    }
}
