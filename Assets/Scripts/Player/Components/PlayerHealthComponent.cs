using UnityEngine;

public class PlayerHealthComponent : MonoBehaviour, IEntity
{
    [SerializeField] private float health = 100f;
    [SerializeField] private GameObject damageTextPrefab;

    private float initialHealth;

    private void Awake()
    {
        initialHealth = health;
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

    public void Hit(float damage)
    {
        Debug.Log("HIT");
        health -= damage;
       
        if (health <=  0)
        {
            Die();
        }

        Color damageColor = DamageColor();
        ShowDamage(damage, damageColor);
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

    private void Die()
    {
        gameObject.GetComponent<PlayerAnimationComponent>().Die();
    }
}
