public abstract class PlayerState
{
    protected PlayerMovementComponent playerMovement;
    protected PlayerCollisionComponent playerCollision;

    public PlayerState(PlayerMovementComponent playerMovement, PlayerCollisionComponent playerCollision)
    {
        this.playerMovement = playerMovement;
        this.playerCollision = playerCollision;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}