using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementComponent : MonoBehaviour
{
    public event Action<Vector2> OnMove;
    public event Action OnJump;
    public event Action OnGrounded;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask platformLayer;
    
    private Rigidbody2D _rb;
    private BoxCollider2D _playerCollider;
    private PlayerState _currentState;

    public float HorizontalInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool _isGrounded { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _playerCollider = GetComponent<BoxCollider2D>();
        ChangeState(new IdleState(this)); 
    }

    private void FixedUpdate()
    {
        _isGrounded = CheckGrounded();
    }

    private void Update()
    {
        _currentState?.Update();
    }

    public void MoveEvent()
    {
        OnMove?.Invoke(_rb.linearVelocity);
    }

    public void JumpUpEvent()
    {
        OnJump?.Invoke();
    }

    public void JumpDownEvent()
    {
        JumpInput = false;
        OnGrounded?.Invoke();
    }

    private bool CheckGrounded()
    {
         Vector2 raycastOrigin = _playerCollider.bounds.center;
         raycastOrigin.y -= _playerCollider.bounds.extents.y;

         RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.1f, platformLayer);
         
         bool grounded = hit.collider != null;
         
         return (grounded);
    }

    public void Move(float horizontalInput)
    {
        Vector2 velocity = _rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;
        
        _rb.linearVelocityX = velocity.x;
         HorizontalInput = horizontalInput;
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            JumpInput = true;
        } 
    }

    public void ChangeState(PlayerState state)
    {
        Debug.Log("State: " + state);
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}
