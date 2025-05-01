using System;
using UnityEngine;

public class SnakeCollisionComponent : MonoBehaviour
{
    [SerializeField] private float distance = 7f;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask playerAndPlatformLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float cooldownTime = 4f;

    private Transform playerTransform;
    private PolygonCollider2D _playerCollider;
    private SpriteRenderer _spriteRenderer;

    private bool finishedAttack = false;
    private float nextAttackTime = 0f;
    private float playerDirection;

    public Action Attack;
    public Action DefaultMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = player.transform;
        _playerCollider = GetComponent<PolygonCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCollision();
    }

    public void CheckCollision()
    {

        float horizontalDistance = Mathf.Abs(playerTransform.position.x - transform.position.x);

        if (horizontalDistance <= distance)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, playerAndPlatformLayer);

            if (hit.collider != null)
            {
                Debug.Log("IN A HIT: " + hit);
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("PLAYER HIT");

                    PlayerInRange();
                    float playerDirection = transform.position.x > hit.collider.transform.position.x ? 1 : -1;
                    Vector3 playerPos = new Vector3(hit.collider.transform.position.x + playerDirection, transform.position.y, transform.position.z);
                    transform.position = Vector2.MoveTowards(transform.position, playerPos, Time.deltaTime);
                   
                    this.playerDirection = playerDirection;
                    flipSprite(hit.collider.transform);
                    checkNextToPlayer(playerDirection);
                }
            }
        } else
        {
            PlayerOutOfRange();
        }

        if (Time.time > nextAttackTime)
        {
            nextAttackTime += cooldownTime;
            finishedAttack = true;
        }
    }

    private void flipSprite(Transform playerTransform)
    {
        _spriteRenderer.flipX = transform.position.x < playerTransform.transform.position.x;
    }

    private void checkNextToPlayer(float direction)
    {
        direction = -direction;
        Vector2 raycastOrigin = new Vector2(_playerCollider.bounds.center.x + direction * _playerCollider.bounds.extents.x, _playerCollider.bounds.center.y);
        RaycastHit2D wallHit = Physics2D.Raycast(raycastOrigin, Vector2.right * direction, 0.3f, playerLayer);
        Debug.DrawRay(raycastOrigin, Vector2.right * direction * 0.3f, Color.red);
        if (wallHit.collider != null)
        {
            if (finishedAttack && (Player.Instance == null || !Player.Instance.IsDead))
            {
                gameObject.SendMessage("Attack");
            }
        }
    }

    public void checkHitPlayer()
    {
        float direction = this.playerDirection;
        direction = -direction;
        Vector2 raycastOrigin = new Vector2(_playerCollider.bounds.center.x + direction * _playerCollider.bounds.extents.x, _playerCollider.bounds.center.y);
        RaycastHit2D wallHit = Physics2D.Raycast(raycastOrigin, Vector2.right * direction, 0.3f, playerLayer);

        if (wallHit.collider != null)
        {
            if (Player.Instance == null || !Player.Instance.IsDead)
            {
                wallHit.collider.gameObject.SendMessage("Hit", 20);
            }
        }

        finishedAttack = false;
    }

    public void PlayerInRange()
    {
        Attack?.Invoke();
    }

    public void PlayerOutOfRange()
    {
        Debug.Log("Player out of range");
        DefaultMovement?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        if (enabled)
        {
            Gizmos.color = Color.yellow;
            Vector3 position = transform.position;

            // Draw horizontal line showing check distance
            Gizmos.DrawLine(
                new Vector3(position.x - distance, position.y, 0),
                new Vector3(position.x + distance, position.y, 0)
            );
        }
    }
}
