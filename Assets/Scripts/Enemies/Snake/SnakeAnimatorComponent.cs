using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeAnimationComponent : MonoBehaviour
{

    private Rigidbody2D _rb;

    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void updateXVelocity()
    {
        _animator.SetFloat("xVelocity", 1);
    }

    public void Idle()
    {
        _animator.SetFloat("xVelocity", 0);
    }

    public void Hurt()
    {
        _animator.SetTrigger("Hurt");
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
    }
    
}
