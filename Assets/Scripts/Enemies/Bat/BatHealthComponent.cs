using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BatHealthComponent : MonoBehaviour, IEntity
{
    
    public float health { get; private set; }
    [SerializeField] private GameObject damageTextPrefab;

    public float initialHealth { get; private set; }
    private BatAnimationComponent _animator;

    private void Awake()
    {
        health = 30f;
        initialHealth = health;
        _animator = GetComponent<BatAnimationComponent>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }

        Color damageColor = DamageColor();
        ShowDamage(damage, damageColor);
    }

    private void Die()
    {
        _animator.Die();
    }

    public Color DamageColor()
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

    public void ShowDamage(float damage, Color damageColor)
    {
            Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            GameObject damageText = Instantiate(damageTextPrefab, transform.position + randomOffset, Quaternion.identity);
            FloatingDamageComponent damageComponent = damageText.GetComponent<FloatingDamageComponent>();
            Debug.Log(damageComponent);
            if (damageComponent != null)
            {
                damageComponent.SetDamage(damage, damageColor);
            }
    }
    
}
