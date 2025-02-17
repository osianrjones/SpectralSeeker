using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovementComponent), typeof(PlayerCollisionComponent))]
public class PlayerInputComponent : MonoBehaviour
{
    private PlayerMovementComponent _playerMovement;
    private PlayerCollisionComponent _playerCollision;
    private InventoryManager _inventoryManager;
    private Keyboard keyboard;
    private bool hasJumped = false;
    private bool hasJumpedOffWall = false;
    private bool disableMovement = false;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovementComponent>();
        _playerCollision = GetComponent<PlayerCollisionComponent>();
        _inventoryManager = GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;

        if (keyboard == null || mouse == null)
        {
            return;
        }
       
        
        if (!disableMovement)
        {
            float horizontal = keyboard.dKey.ReadValue()
                           - keyboard.aKey.ReadValue();

            _playerMovement.Move(horizontal);

            flipSprite(horizontal);
        }

        for (int i = 1; i <= 5; i++)
        {
            switch (i)
            {
                case 1:
                    if (keyboard.digit1Key.wasPressedThisFrame) 
                    { 
                        inventoryPressed(i);
                        disableMovement = !disableMovement;
                    }               
                    break;
                case 2:
                    if (keyboard.digit2Key.wasPressedThisFrame) { inventoryPressed(i); }
                    break;
                case 3: if (keyboard.digit3Key.wasPressedThisFrame) { inventoryPressed(i);}
                    break;
                default:
                    break;
            }
        }
        
    }

    private void inventoryPressed(int v)
    {
        _inventoryManager.inventoryPressed(v);
    }

    private void flipSprite(float horizontal)
    {
        if (horizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void FixedUpdate()
    {
        if (keyboard != null)
        {
            if (keyboard.spaceKey.isPressed && _playerCollision.isGrounded && !hasJumped)
            {
                hasJumped = true;
                _playerMovement.Jump();
            } 
            else if (keyboard.spaceKey.isPressed && _playerCollision.isWalled && !hasJumpedOffWall)
            {
                hasJumpedOffWall = true;
                _playerMovement.JumpOffWall();
            }
            else if (!keyboard.spaceKey.isPressed)
            {
                hasJumped = false;
            }
        }

        if (_playerCollision.isGrounded)
        {
            hasJumpedOffWall = false;
        }
    }
}
