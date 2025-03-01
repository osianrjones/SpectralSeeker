using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FloatingDamageComponent : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // Speed at which the text moves upward
    [SerializeField] private float fadeDuration = 1f; // Duration before the text fades out
    [SerializeField] private TextMeshPro damageText; // Reference to the TextMeshPro component

    private float fadeTimer;
    private bool destroyable = false;
    private Color damageColor;
    
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
         
        float alpha = fadeTimer / fadeDuration; // Calculate alpha based on remaining time
        damageText.color = new Color(damageColor.r, damageColor.g, damageColor.b, alpha);
        if (alpha <= 0f && destroyable)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage, Color color)
    {
        damageText.text = String.Format("{0}", damage);
        destroyable = true;
        damageColor = color;
    }
    
}