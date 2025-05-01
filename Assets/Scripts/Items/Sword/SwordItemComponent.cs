using System;
using UnityEngine;

public class SwordItemComponent : MonoBehaviour, IItem
{
    [SerializeField] private int swordDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask wallLayer;
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

    public bool ToggleUse()
    {
        ServiceLocator.Get<ISoundService>().PlaySFX(drawSword);
        isHolding = !isHolding;
        animator.ToggleSword();
        return true;
    }

    public bool Holding()
    {
        return isHolding;
    }

    public void Attack()
    {
        ServiceLocator.Get<ISoundService>().PlaySFX(swingSword);
        float direction = playerSprite.flipX ? -1f : 1f;

        // Use RaycastAll to get all objects in the path - allows checking both enemies and walls
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right * direction, attackRange);

        // Sort hits by distance to ensure we hit closest objects first
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit2D hit in hits)
        {
            if ((wallLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
            { 
                break;
            }

            // Check if it's an enemy
            if ((enemyLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
            {
                hit.collider.gameObject.SendMessage("TakeDamage", swordDamage, SendMessageOptions.DontRequireReceiver);
                hit.collider.gameObject.SendMessage("Hurt", SendMessageOptions.DontRequireReceiver);
                break; // Only hit the first enemy
            }
        }
    }
}
