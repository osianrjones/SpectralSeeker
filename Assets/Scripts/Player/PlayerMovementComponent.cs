using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementComponent : MonoBehaviour
{
    public event Action<float> OnMove;
    public event Action OnJump;
    public event Action OnGrounded;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask platformLayer;
    
    private Rigidbody2D _rb;
    private BoxCollider2D _playerCollider;
    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _playerCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        _isGrounded = CheckGrounded();
    }

    private bool CheckGrounded()
    {
         Vector2 raycastOrigin = _playerCollider.bounds.center;
         raycastOrigin.y -= _playerCollider.bounds.extents.y;

         RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.1f, platformLayer);
         
         bool grounded = hit.collider != null;

         if (grounded)
         {
             OnGrounded?.Invoke();
         }
         
         return (grounded);
    }

    public void Move(float horizontalInput)
    {
        Vector2 velocity = _rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;
        
        _rb.linearVelocityX = velocity.x;
        
        OnMove?.Invoke(_rb.linearVelocityX);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            OnJump?.Invoke();
        }
    }
}
