using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovementComponent))]
public class PlayerInputComponent : MonoBehaviour
{
    private PlayerMovementComponent _playerMovement;
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovementComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;

        if (keyboard == null || mouse == null)
        {
            return;
        }
        
        float horizontal = keyboard.dKey.ReadValue()
                           - keyboard.aKey.ReadValue();
        
        _playerMovement.Move(horizontal);

        if (keyboard.spaceKey.isPressed)
        {
            _playerMovement.Jump();
        }
        
    }
}
