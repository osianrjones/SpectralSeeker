using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BatHealthComponent : MonoBehaviour
{
    
    [SerializeField] private float health;
    [SerializeField] private GameObject damageTextPrefab;

    private float initialHealth;
    private BatAnimationComponent _animatior;

    private void Awake()
    {
        initialHealth = health;
        _animatior = GetComponent<BatAnimationComponent>();
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
        _animatior.Die();
    }

    private Color DamageColor()
    {
        Debug.Log(health);
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

    private void ShowDamage(float damage, Color damageColor)
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
