using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private bool _isFacingRight = true;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _moveDirection;
    private Vector2 _moveVelocity;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        MovementHandler();
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveVelocity * Time.fixedDeltaTime);
        _animator.SetBool("isMoving", (Mathf.Abs(_moveDirection.x) > 0 || Mathf.Abs(_moveDirection.y) > 0));
    }

    private void MovementHandler()
    {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveVelocity = _moveDirection.normalized * speed;

        if ((_isFacingRight && _moveDirection.x < 0) || (!_isFacingRight && _moveDirection.x > 0))
        {
            RotateSpriteHandler();
        }
    }
    
    private void RotateSpriteHandler()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
