using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovementComponent), typeof(PlayerCollisionComponent))]
public class PlayerInputComponent : MonoBehaviour
{
    private PlayerMovementComponent _playerMovement;
    private PlayerCollisionComponent _playerCollision;
    private InventoryManager _inventoryManager;
    private PlayerAnimationComponent _playerAnimation;
    private SwordItemComponent _playerSwordItemComponent;
    private Keyboard keyboard;
    private bool hasJumped = false;
    private bool hasJumpedOffWall = false;
    private bool disableMovement = false;

    [SerializeField] private GameObject Menu;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovementComponent>();
        _playerCollision = GetComponent<PlayerCollisionComponent>();
        _inventoryManager = GetComponent<InventoryManager>();
        _playerAnimation = GetComponent<PlayerAnimationComponent>();
        _playerSwordItemComponent = GetComponentInChildren<SwordItemComponent>();
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
                case 4:
                    if (keyboard.digit4Key.wasPressedThisFrame) { inventoryPressed(i); }
                    break;
                case 5:
                    if (keyboard.digit5Key.wasPressedThisFrame) { inventoryPressed(i); }
                    break;
                default:
                    break;
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (_playerSwordItemComponent.Holding() && _playerCollision.isGrounded)
            {
                _playerAnimation.Attack();
            }
        }

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            _inventoryManager.throwItem();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Menu.SetActive(!Menu.activeSelf);
            
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
            } else
            {
                Time.timeScale = 1f;
            }
        }
    }

    private void inventoryPressed(int v)
    {
        _inventoryManager.inventoryPressed(v);
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
