using UnityEngine;

public class SnakeMovementComponent : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 5f; // Speed at which the entity moves
   [SerializeField] private float moveDuration = 3f; // Time in seconds to move before changing direction
   [SerializeField] private LayerMask wallLayer; // LayerMask to detect walls

    private int direction; // Direction of movement: -1 for left, 1 for right
    private float timer;
    private bool isMoving = true;

    void Start()
    {
        // Randomly choose a direction at the start
        ChooseRandomDirection();
        timer = moveDuration;
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

    void ChooseRandomDirection()
    {
        // Randomly pick -1 (left) or 1 (right)
        direction = Random.Range(0, 2) * 2 - 1;
    }

    void MoveEntity()
    {
        // Calculate the movement vector
        Vector3 movement = new Vector3(direction, 0, 0) * moveSpeed * Time.deltaTime;

        // Move the entity
        transform.Translate(movement);
    }

    void ReverseDirection()
    {
        // Reverse the direction
        direction *= -1;
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