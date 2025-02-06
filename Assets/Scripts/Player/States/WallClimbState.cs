using UnityEngine;

public class WallClimbState : PlayerState
{
    public WallClimbState(PlayerMovementComponent player) : base(player) { }

    public override void Update()
    {
        // Transition to Idle (case user slides to bottom of wall)
        
        
        // Transition to Jumping (case user jumps off wall)
        
    }
}