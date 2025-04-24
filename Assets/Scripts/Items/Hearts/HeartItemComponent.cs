using UnityEngine;

public class HeartItemComponent : MonoBehaviour, IItem
{
    [SerializeField] private PlayerHealthComponent playerHealth;
    [SerializeField] private int healthValue = 25;
    [SerializeField] private AudioClip useSound;

    public bool ToggleUse()
    {
        if (playerHealth.health < 100)
        {
            ServiceLocator.Get<ISoundService>().PlaySFX(useSound);
            playerHealth.Heal(healthValue);
            return true;
        }
        return false;
    }
}
