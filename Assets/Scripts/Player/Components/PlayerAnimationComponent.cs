using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationComponent : MonoBehaviour
{
    private Animator _animator;
    private AnimatorController _defaultController;
    [SerializeField] private AnimatorOverrideController animatorOverride;
    private Rigidbody2D rb;
    private bool swordEquipped = false;
    
    public event Action Attacked;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _defaultController = _animator.runtimeAnimatorController as AnimatorController;
    }

    private void Update()
    {
        _animator.SetFloat("yVelocity", rb.linearVelocity.y);
        _animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));

    }

    public void Subscribe(PlayerMovementComponent _movementComponent)
    {
        _movementComponent.OnJump += HandleJumpUp;
        _movementComponent.OnGrounded += HandleJumpDown;
        _movementComponent.OnWall += OnWall;
        _movementComponent.OffWall += OffWall;
    }

    public void ToggleSword()
    {
        if (!swordEquipped)
        {
            if (animatorOverride != null)
            {
                _animator.runtimeAnimatorController = animatorOverride;
            }

            swordEquipped = true;
        }
        else
        {
            _animator.runtimeAnimatorController = _defaultController;
            swordEquipped = false;
        }

    }
    
    public void Attack() 
    {
        _animator.SetBool("attack", true);
        
     }

    public void ResetAttack()
    {
        _animator.SetBool("attack", false);
        Attacked?.Invoke();
    }

    //jump
    public void HandleJumpUp(Vector2 input)
    {
        _animator.SetBool("isJumping", true);
    }

    public void HandleJumpDown(Vector2 input)
    {
        _animator.SetBool("isJumping", false);
    }

    public void OnWall()
    {
        Debug.Log("ON WALL");
        _animator.SetBool("onWall", true);
    }

    public void OffWall()
    {
        Debug.Log("OFF WALL");
        _animator.SetBool("onWall", false);
    }
}
