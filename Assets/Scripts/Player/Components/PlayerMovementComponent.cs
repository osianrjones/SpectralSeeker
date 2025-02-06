using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerCollisionComponent))]
public class PlayerMovementComponent : MonoBehaviour
{
    public event Action<Vector2> OnMove;
    public event Action OnJump;
    public event Action OnGrounded;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float wallSlideSpeed;
    
    private Rigidbody2D _rb;
    private PlayerCollisionComponent _playerCollider;
    private PlayerState _currentState;

    public float HorizontalInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool _isGrounded { get; private set; }
    
    public bool _isWallSliding { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _playerCollider = GetComponent<PlayerCollisionComponent>();
        ChangeState(new IdleState(this, _playerCollider)); 
    }

    private void FixedUpdate()
    {
        //_playerCollider.CheckGrounded();
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

    public void Move(float horizontalInput)
    {
        Vector2 velocity = _rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;
        
        _rb.linearVelocityX = velocity.x;
         HorizontalInput = horizontalInput;
         Debug.Log("HORIZONTAL: " + HorizontalInput);
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
