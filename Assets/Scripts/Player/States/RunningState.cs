using UnityEngine;

public class RunningState : PlayerState
{
    public RunningState(PlayerMovementComponent player) : base(player) { }

    public override void Enter()
    {
        Player.MoveEvent();
    }

    public override void Update()
    {

        // Transition to Idle if there's no horizontal input
        if (Mathf.Abs(Player.HorizontalInput) == 0)
        {
            Player.ChangeState(new IdleState(Player));
        }

        // Transition to Jumping if the player jumps
        if (Player.JumpInput && Player._isGrounded)
        {
            Player.ChangeState(new JumpingState(Player));
        }
    }

    public override void Exit()
    {
        Player.MoveEvent(); // Stop running animation
    }
}