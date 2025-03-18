using UnityEngine;

public class HeartItemComponent : MonoBehaviour, IItem
{
    [SerializeField] private PlayerHealthComponent playerHealth;
    [SerializeField] private int healthValue = 25;

    public void ToggleUse()
    {
        if (playerHealth.health < 100)
        {
            playerHealth.Heal(healthValue);
        }      
    }
}
