using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovementComponent), typeof(PlayerCollisionComponent))]
public class PlayerInputComponent : MonoBehaviour
{
    private PlayerMovementComponent _playerMovement;
    private PlayerCollisionComponent _playerCollision;
    private Keyboard keyboard;
    private bool hasJumped = false;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovementComponent>();
        _playerCollision = GetComponent<PlayerCollisionComponent>();
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
        
        float horizontal = keyboard.dKey.ReadValue()
                           - keyboard.aKey.ReadValue();
        
        _playerMovement.Move(horizontal);

        flipSprite(horizontal);
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
            else if (!keyboard.spaceKey.isPressed)
            {
                hasJumped = false;
            }
        }
    }
}
