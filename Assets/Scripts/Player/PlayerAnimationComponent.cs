using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationComponent : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Subscribe(PlayerMovementComponent _movementComponent)
    {
        _movementComponent.OnMove += HandleMove;
        _movementComponent.OnJump += HandleJumpUp;
        _movementComponent.OnGrounded += HandleJumpDown;
    }
    
    //move
    public void HandleMove(float horizontalInput)
    {
        _animator.SetFloat("xVelocity", horizontalInput);
    }
    
    //jump
    public void HandleJumpUp()
    {
        _animator.SetBool("isJumping", true);
    }

    public void HandleJumpDown()
    {
        _animator.SetBool("isJumping", false);
    }
}
