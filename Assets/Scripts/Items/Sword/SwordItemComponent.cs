using System;
using UnityEngine;

public class SwordItemComponent : MonoBehaviour
{
    private PlayerAnimationComponent animator;
    private bool isHolding = false;

    private void Awake()
    {
        animator = GetComponentInParent<PlayerAnimationComponent>();
    }

    public void ToggleUse()
    {
        isHolding = !isHolding;
        animator.ToggleSword();
    }
}
