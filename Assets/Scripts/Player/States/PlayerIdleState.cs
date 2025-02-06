using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerMovementComponent playerMovement, PlayerCollisionComponent playerCollision) : base(playerMovement, playerCollision) { }

    public override void Update()
    {
        // Transition to Running if there's horizontal input
        if (Mathf.Abs(playerMovement.HorizontalInput) != 0)
        {
            playerMovement.ChangeState(new RunningState(playerMovement, playerCollision));
        }

        if (playerMovement.JumpInput)
        {
            Debug.Log("JUMPED BUT GROUNDED?: " + playerCollision.isGrounded);
        }

        // Transition to Jumping if the player jumps
        if (playerMovement.JumpInput && !playerCollision.isGrounded)
        {
            playerMovement.ChangeState(new JumpingState(playerMovement, playerCollision));
        }
    }
}