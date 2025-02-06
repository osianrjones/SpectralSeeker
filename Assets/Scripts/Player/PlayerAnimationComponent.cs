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
    public void HandleMove(Vector2 input)
    {
        _animator.SetFloat("xVelocity", Math.Abs(input.x));
        _animator.SetFloat("yVelocity", input.y);
    }
    
    //jump
    public void HandleJumpUp()
    {
        Debug.Log("HANDLE JUMP UP");
        _animator.SetBool("isJumping", true);
    }

    public void HandleJumpDown()
    {
        _animator.SetBool("isJumping", false);
    }
}
