using UnityEngine;

public class WallSlideState : PlayerState
{
    public WallSlideState(PlayerMovementComponent playerMovement, PlayerCollisionComponent playerCollision) : base(playerMovement, playerCollision) { }

    public override void Enter()
    {
        playerMovement.OnWallEvent();
    }

    public override void Update()
    {
        // Transition to Idle (case user slides to bottom of wall)
        if (playerCollision.isGrounded)
        {
            playerMovement.ChangeState(new IdleState(playerMovement, playerCollision));
        }

        // Transition to Jumping (case user jumps off wall)

        if (!playerCollision.isWalled && !playerCollision.isGrounded) 
        {
            playerMovement.ChangeState(new JumpingState(playerMovement, playerCollision));
        }
    }

    public override void Exit() 
    {
        playerMovement.OffWallEvent();
    }
}