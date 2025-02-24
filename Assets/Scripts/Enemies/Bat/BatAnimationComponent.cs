using UnityEngine;

[RequireComponent (typeof(Animator))]
public class BatAnimationComponent : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void ResetAttack()
    {
        animator.ResetTrigger("Attack");
    }
}
