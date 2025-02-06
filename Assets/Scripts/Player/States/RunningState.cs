using UnityEngine;

public class RunningState : PlayerState
{
    public RunningState(PlayerMovementComponent playerMovement, PlayerCollisionComponent playerCollision) : base(playerMovement, playerCollision) { }

    public override void Enter()
    {
        playerMovement.MoveEvent();
    }

    public override void Update()
    {

        // Transition to Idle if there's no horizontal input
        if (Mathf.Abs(playerMovement.HorizontalInput) == 0)
        {
            playerMovement.ChangeState(new IdleState(playerMovement, playerCollision));
        }

        // Transition to Jumping if the player jumps
        if (playerMovement.JumpInput && playerMovement._isGrounded)
        {
            playerMovement.ChangeState(new JumpingState(playerMovement, playerCollision));
        }
    }

    public override void Exit()
    {
        playerMovement.MoveEvent(); // Stop running animation
    }
}