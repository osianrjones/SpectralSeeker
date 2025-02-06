using System;
using UnityEngine;
using UnityEngine.Windows;

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
    public void HandleMove(Vector2 input)
    {
        _animator.SetFloat("xVelocity", Math.Abs(input.x));
    }
    
    //jump
    public void HandleJumpUp(Vector2 input)
    {
        Debug.Log("HANDLE JUMP UP");
        _animator.SetBool("isJumping", true);
        _animator.SetFloat("yVelocity", input.y);
    }

    public void HandleJumpDown(Vector2 input)
    {
        _animator.SetBool("isJumping", false);
        _animator.SetFloat("yVelocity", input.y);
    }
}
