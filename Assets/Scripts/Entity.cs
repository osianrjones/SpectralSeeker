using UnityEngine;

public abstract class EntityHealth : MonoBehaviour
{
    [SerializeField] protected float health = 100f;

    protected float initialHealth;

    protected virtual void Start()
    {
        initialHealth = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
            return;
        }

        Color damageColor = GetDamageColor();
        ShowDamage(damage, damageColor);
    }

    // Subclasses must implement
    protected abstract Color GetDamageColor();
    protected abstract void ShowDamage(float damage, Color damageColor);

    // Subclasses can override
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
