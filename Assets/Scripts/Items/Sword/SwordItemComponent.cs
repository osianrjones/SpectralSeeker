using System;
using UnityEngine;

public class SwordItemComponent : MonoBehaviour, IItem
{
    [SerializeField] private int swordDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioClip drawSword;
    [SerializeField] private AudioClip swingSword;

    private PlayerAnimationComponent animator;
    private bool isHolding = false;    
    private SpriteRenderer playerSprite;

    private void Start()
    {
        animator = GetComponentInParent<PlayerAnimationComponent>();
        playerSprite = GetComponentInParent<SpriteRenderer>();
        animator.Attacked += Attack;
    }

    public void ToggleUse()
    {
        ServiceLocator.Get<ISoundService>().PlaySFX(drawSword);
        isHolding = !isHolding;
        animator.ToggleSword();
    }

    public bool Holding()
    {
        return isHolding;
    }

    public void Attack()
    {
        ServiceLocator.Get<ISoundService>().PlaySFX(swingSword);
        float direction = playerSprite.flipX ? -1f : 1f;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, attackRange, enemyLayer);
        
        if (hit.collider != null)
        {
            hit.collider.gameObject.SendMessage("TakeDamage", swordDamage);
            hit.collider.gameObject.SendMessage("Hurt");
        }
    }
}
