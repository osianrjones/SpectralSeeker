using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public bool IsDead { get; private set; } = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerMovementComponent _movementComponent = GetComponent<PlayerMovementComponent>();
        PlayerAnimationComponent _animationComponent = GetComponent<PlayerAnimationComponent>();

        _animationComponent.Subscribe(_movementComponent);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void WrapUpPlayer()
    {
        IsDead = true;
        gameObject.GetComponentInChildren<FlashlightItemComponent>().TurnOff();   
        StartCoroutine(FadeOut(1f));
    }

    IEnumerator FadeOut(float duration)
    {
        float startAlpha = spriteRenderer.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // Update the elapsed time
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration); // Calculate the new alpha
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                newAlpha); // Update the color
            yield return null; // Wait for the next frame
        }


        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        Time.timeScale = 0f;

        MenuManager.BackToMainMenu();
    }

    public float FacingDirection()
    {
        return spriteRenderer.flipX ? -1 : 1;
    }
}
