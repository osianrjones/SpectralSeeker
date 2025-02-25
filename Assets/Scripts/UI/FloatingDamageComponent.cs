using UnityEngine;
using TMPro;

public class FloatingDamageComponent : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // Speed at which the text moves upward
    [SerializeField] private float fadeDuration = 1f; // Duration before the text fades out
    [SerializeField] private TextMeshProUGUI damageText; // Reference to the TextMeshPro component

    private float fadeTimer;

    void Start()
    {
        fadeTimer = fadeDuration;
    }

    void Update()
    {
        // Move the text upward
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Fade out the text
        fadeTimer -= Time.deltaTime;
        if (fadeTimer <= 0)
        {
            Destroy(gameObject); // Destroy the text when the fade duration is over
        }
        else
        {
            float alpha = fadeTimer / fadeDuration; // Calculate alpha based on remaining time
            damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, alpha);
        }
    }

    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString(); // Set the damage value
    }
}