using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        PlayerMovementComponent _movementComponent = GetComponent<PlayerMovementComponent>();
        PlayerAnimationComponent _animationComponent = GetComponent<PlayerAnimationComponent>();
        
        _animationComponent.Subscribe(_movementComponent);
    }
}
