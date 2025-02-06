using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerMovementComponent player) : base(player) { }

    public override void Update()
    {
        // Transition to Running if there's horizontal input
        if (Mathf.Abs(Player.HorizontalInput) != 0)
        {
            Player.ChangeState(new RunningState(Player));
        }

        // Transition to Jumping if the player jumps
        if (Player.JumpInput && Player._isGrounded)
        {
            Player.ChangeState(new JumpingState(Player));
        }
    }
}