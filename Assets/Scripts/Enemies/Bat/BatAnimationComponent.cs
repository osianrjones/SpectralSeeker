using UnityEngine;

[RequireComponent (typeof(Animator))]
public class BatAnimationComponent : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void ResetAttack()
    {
        animator.ResetTrigger("Attack");
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void ResetHurt()
    {
        animator.ResetTrigger("Hurt");
    }
    
    
}
