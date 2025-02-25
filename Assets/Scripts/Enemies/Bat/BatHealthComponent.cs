using UnityEngine;

public class BatHealthComponent : MonoBehaviour
{
    [SerializeField] private float health;

    public void Damage(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
