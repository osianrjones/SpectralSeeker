using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SnakeMovementComponent : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 5f; // Speed at which the entity moves
   [SerializeField] private float moveDuration = 3f; // Time in seconds to move before changing direction
   [SerializeField] private LayerMask wallLayer; // LayerMask to detect walls

    private int direction = 1; // Direction of movement: -1 for left, 1 for right
    private float timer;
    private bool isMoving = true;
    private SnakeAnimationComponent _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    
    void Start()
    {
        // Randomly choose a direction at the start
        ChooseRandomDirection();
        timer = moveDuration;
        _animator = GetComponent<SnakeAnimationComponent>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        GetComponent<SnakeHealthComponent>().Death += HandleDeath;
        GetComponent<SnakeCollisionComponent>().Attack += AttackPlayer;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveEntity();

            // Update the timer
            timer -= Time.deltaTime;
            if (timer <= 0 || CheckForWall())
            {
                ReverseDirection();
                timer = moveDuration; // Reset the timer
            }
        }
    }

    private void HandleDeath()
    {
        this.isMoving = false;
    }

    private void AttackPlayer(Transform player)
    {
        direction = transform.position.x > player.transform.position.x ? 1 : -1;
        Vector3 playerPos = new Vector3(player.transform.position.x + direction, player.transform.position.y, player.transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);
    }

    void ChooseRandomDirection()
    {
        // Randomly pick -1 (left) or 1 (right)
        direction = Random.Range(0, 2) * 2 - 1;
    }

    void MoveEntity()
    { 
        _rb.linearVelocity = new Vector2(direction * moveSpeed, _rb.linearVelocity.y);
    }

    void ReverseDirection()
    {
        // Reverse the direction
        direction *= -1;
        _sr.flipX = direction > 0;
    }

    bool CheckForWall()
    {
        // Cast a ray in the direction of movement to detect walls
        float rayLength = 0.3f; // Adjust based on the size of your entity
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(direction, 0), rayLength, wallLayer);
        
        // If a wall is detected, return true
        return hit.collider != null;
    }
}