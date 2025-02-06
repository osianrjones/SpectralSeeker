using System;
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
        Vector2 raycastOrigin = _playerCollider.bounds.center;
        RaycastHit2D wallHit = Physics2D.Raycast(raycastOrigin, new Vector2(_rb.linearVelocityX, 0), 0.5f, platformLayer);
        
        return wallHit.collider != null;
    }
}
