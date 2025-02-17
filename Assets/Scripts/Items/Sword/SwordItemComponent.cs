using System;
using UnityEngine;

public class SwordItemComponent : MonoBehaviour
{
    private PlayerAnimationComponent animator;
    private bool isHolding = false;
    [SerializeField] private int swordDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    private SpriteRenderer playerSprite;
    
    private void Awake()
    {
        animator = GetComponentInParent<PlayerAnimationComponent>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ToggleUse()
    {
        isHolding = !isHolding;
        animator.ToggleSword();
    }

    public bool Holding()
    {
        return isHolding;
    }

    public void Attack()
    {
        float direction = playerSprite.flipX ? -1f : 1f;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, attackRange, enemyLayer);

        if (hit != null)
        {
            //implement hit logic
        }
    }
}
