using System;
using NUnit.Framework.Constraints;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PlayerCollisionComponent : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayer;
    
    private BoxCollider2D _playerCollider;
    private Rigidbody2D _rb;
    
    public bool isGrounded { get; private set; }
    public bool isWalled { get; private set; }
    
    private void Awake()
    {
        _playerCollider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = CheckGrounded();
        isWalled = CheckWalled();
    }

    public bool CheckGrounded()
    {
        Vector2 raycastOrigin = _playerCollider.bounds.center;
        raycastOrigin.y -= _playerCollider.bounds.extents.y;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.1f, platformLayer);
         
        bool grounded = hit.collider != null;
        return (grounded);
    }

    public bool CheckWalled()
    {
        float direction = gameObject.GetComponent<SpriteRenderer>().flipX ? -1f : 1f;

        Vector2 raycastOrigin = new Vector2(_playerCollider.bounds.center.x + direction * _playerCollider.bounds.extents.x, _playerCollider.bounds.center.y);
       
        RaycastHit2D wallHit = Physics2D.Raycast(raycastOrigin, Vector2.right * direction, 0.1f, platformLayer);

        Debug.DrawRay(raycastOrigin, Vector2.right * direction * 0.1f, Color.red);

        return wallHit.collider != null;
    }
}
