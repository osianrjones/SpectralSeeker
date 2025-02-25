using System;
using UnityEngine;

public class BatCollisionComponent : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speed;

    private bool nextToPlayer = false;
    private PolygonCollider2D _playerCollider;

    private void Awake()
    {
        _playerCollider = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                float direction = transform.position.x > collider.transform.position.x ? 1 : -1;
                Vector3 playerPos = new Vector3(collider.transform.position.x + direction, collider.transform.position.y, collider.transform.position.z);
                transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
                flipSprite(collider.transform);
                checkNextToPlayer(direction);
            }
        }
    }

    private void checkNextToPlayer(float direction)
    {
        direction = -direction;
        Vector2 raycastOrigin = new Vector2(_playerCollider.bounds.center.x + direction * _playerCollider.bounds.extents.x, _playerCollider.bounds.center.y);
        RaycastHit2D wallHit = Physics2D.Raycast(raycastOrigin, Vector2.right * direction, 0.3f, playerLayer);
        Debug.DrawRay(raycastOrigin, Vector2.right * direction * 0.3f, Color.red);
        if (wallHit.collider != null)
        {
            gameObject.SendMessage("Attack");
            wallHit.collider.gameObject.SendMessage("Hit", 20);
        }
    }

    private void flipSprite(Transform playerTransform)
    {
        if (transform.position.x > playerTransform.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
