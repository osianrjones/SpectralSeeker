using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerCollisionComponent))]
public class PlayerMovementComponent : MonoBehaviour
{
    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnJump;
    public event Action<Vector2> OnGrounded;
    public event Action OnWall;
    public event Action OffWall;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float wallSlideSpeed;
    
    private Rigidbody2D _rb;
    private PlayerCollisionComponent _playerCollider;
    private PlayerState _currentState;

    public float HorizontalInput { get; private set; }
    public bool JumpInput { get; set; }
    public bool _isGrounded { get; private set; }
    
    public bool _isWallSliding { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _playerCollider = GetComponent<PlayerCollisionComponent>();
        ChangeState(new IdleState(this, _playerCollider)); 
        JumpInput = false;
    }

    private void Update()
    {
        _currentState?.Update();
        WallSlide();
    }

    public void OnWallEvent()
    {
        OnWall?.Invoke();
    }

    public void OffWallEvent()
    {
        OffWall?.Invoke();
    }

    public void WallSlide()
    {
        if (_playerCollider.isWalled && !_playerCollider.isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, Mathf.Clamp(_rb.linearVelocity.y, -wallSlideSpeed, float.MaxValue));
            JumpInput = false;
        }
    }

    public void JumpOffWall()
    {
        float facingDirection = Mathf.Sign(transform.localScale.x);

        var wallJumpDirection = -facingDirection;
        _rb.linearVelocity = new Vector2(wallJumpDirection * jumpForce, jumpForce);
        OffWallEvent();
    }

    public void MoveEvent()
    {
        OnMove?.Invoke(_rb.linearVelocity);
    }

    public void JumpUpEvent()
    {
        OnJump?.Invoke(_rb.linearVelocity);
    }

    public void JumpDownEvent()
    {
        OnGrounded?.Invoke(_rb.linearVelocity);
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
        if (_playerCollider.isGrounded)
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
