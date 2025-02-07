using UnityEngine;

public class JumpingState : PlayerState
{
    public JumpingState(PlayerMovementComponent playerMovement, PlayerCollisionComponent playerCollision) : base(playerMovement, playerCollision) { }

    public override void Enter()
    {
        playerMovement.JumpUpEvent();
    }

    public override void Update()
    {
        // Transition to Idle or Running when landing
        if (playerCollision.isGrounded)
        {
            playerMovement.JumpInput = false;
            if (Mathf.Abs(playerMovement.HorizontalInput) > 0.1f)
            {
                playerMovement.ChangeState(new RunningState(playerMovement, playerCollision));
            }
            else
            {
                playerMovement.ChangeState(new IdleState(playerMovement, playerCollision));
            }
        }

        if (playerCollision.isWalled && !playerCollision.isGrounded)
        {
            playerMovement.ChangeState(new WallSlideState(playerMovement, playerCollision));
        }
    }

    public override void Exit()
    {
        playerMovement.JumpDownEvent();
    }
}