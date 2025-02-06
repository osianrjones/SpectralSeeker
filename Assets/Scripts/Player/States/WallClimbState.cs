using UnityEngine;

public class WallClimbState : PlayerState
{
    public WallClimbState(PlayerMovementComponent playerMovement, PlayerCollisionComponent playerCollision) : base(playerMovement, playerCollision) { }

    public override void Update()
    {
        // Transition to Idle (case user slides to bottom of wall)
        
        
        // Transition to Jumping (case user jumps off wall)
        
    }
}