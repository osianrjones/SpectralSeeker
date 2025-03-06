using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image backgroundBarFill;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private PlayerHealthComponent entity;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (entity != null && healthBarFill != null)
        {
            if (entity)
            {
                healthBarFill.fillAmount = entity.health / entity.initialHealth;
                Debug.Log("Fill amount: " + healthBarFill.fillAmount);
            }
            
            if (healthBarFill.fillAmount >= 1f)
            {
                Color color1 = healthBarFill.color;
                color1.a = 0f;
                healthBarFill.color = color1;
                
                Color color2 = backgroundBarFill.color;
                color2.a = 0f;
                backgroundBarFill.color = color2;
            }
            else
            {
                Color color1 = healthBarFill.color;
                color1.a = 255f;
                healthBarFill.color = color1;
                
                Color color2 = backgroundBarFill.color;
                color2.a = 255f;
                backgroundBarFill.color = color2;

                if (healthBarFill.fillAmount < 0.25f)
                {
                    Color lowHealth = Color.red;
                    healthBarFill.color = lowHealth;
                }
                else
                {
                    Color normalHealth = Color.green;
                    healthBarFill.color = normalHealth;
                }
            }
        }
    }
}
