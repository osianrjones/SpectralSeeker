public abstract class PlayerState
{
    protected PlayerMovementComponent Player;

    public PlayerState(PlayerMovementComponent player)
    {
        Player = player;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}