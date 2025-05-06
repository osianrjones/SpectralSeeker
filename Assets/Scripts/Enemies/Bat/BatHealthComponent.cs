using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BatHealthComponent : EntityHealth
{
    
    [SerializeField] private GameObject damageTextPrefab;

    private BatAnimationComponent _animator;

    private void Awake()
    {
        health = 30f;
        initialHealth = health;
        _animator = GetComponent<BatAnimationComponent>();
    }

    protected override void Die()
    {
        _animator.Die();
    }

    protected override Color GetDamageColor()
    {
        if (health <= 0f)
        {
            return Color.red;
        }
        if (health > 0 && health <= initialHealth / 2)
        {
            return Color.yellow;
        }
        return Color.green;
    }

    protected override void ShowDamage(float damage, Color damageColor)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        GameObject damageText = Instantiate(damageTextPrefab, transform.position + randomOffset, Quaternion.identity);
        FloatingDamageComponent damageComponent = damageText.GetComponent<FloatingDamageComponent>();
        if (damageComponent != null)
        {
            damageComponent.SetDamage(damage, damageColor);
        }
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetInitialHealth()
    {
        return initialHealth;
    }
}
