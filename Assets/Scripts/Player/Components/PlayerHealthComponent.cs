using UnityEngine;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    public void Hit(float damage)
    {
        health -= damage;
    }
}
