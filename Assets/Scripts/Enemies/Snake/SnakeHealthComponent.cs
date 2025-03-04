using System;
using UnityEngine;

public class SnakeHealthComponent : MonoBehaviour
{
    [SerializeField] private float health = 20;
    [SerializeField] private GameObject damageTextPrefab;

    private float _initialHealth;
    private SnakeAnimationComponent _animator;

    public Action Death;

    private void Awake()
    {
        _initialHealth = health;
        _animator = GetComponent<SnakeAnimationComponent>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("DIE");
            InvokeDeathEvent();
            Die();
        }

        Color damageColor = DamageColor();
        ShowDamage(damage, damageColor);
    }

    private void InvokeDeathEvent()
    {
        Death?.Invoke();
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
        if (health > 0 && health <= _initialHealth / 2)
        {
            return Color.yellow;
        }
        return Color.green;
    }

    public void ShowDamage(float damage, Color damageColor)
    {
        Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0);
        GameObject damageText = Instantiate(damageTextPrefab, transform.position + randomOffset, Quaternion.identity);
        FloatingDamageComponent damageComponent = damageText.GetComponent<FloatingDamageComponent>();
        Debug.Log(damageComponent);
        if (damageComponent != null)
        {
            damageComponent.SetDamage(damage, damageColor);
        }
    }
}
