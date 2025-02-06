using UnityEngine;

public class JumpingState : PlayerState
{
    public JumpingState(PlayerMovementComponent player) : base(player) { }

    public override void Enter()
    {
        Player.JumpUpEvent();
    }

    public override void Update()
    {
        // Transition to Idle or Running when landing
        if (Player._isGrounded)
        {
            if (Mathf.Abs(Player.HorizontalInput) > 0.1f)
            {
                Player.ChangeState(new RunningState(Player));
            }
            else
            {
                Player.ChangeState(new IdleState(Player));
            }
        }
    }

    public override void Exit()
    {
        Player.JumpDownEvent();
    }
}