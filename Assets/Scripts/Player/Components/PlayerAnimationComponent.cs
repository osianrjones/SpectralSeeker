using System;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationComponent : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
