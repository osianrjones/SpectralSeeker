using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreComponent : MonoBehaviour
{
    public int score { get; private set; }
    public static PlayerScoreComponent Instance { get; private set; }

    private int targetScore;

    [SerializeField] private float scoreSpeed = 1f;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        score = 0;
        targetScore = 0;
    }

    public void IncrementScore(int amount)
    {
        targetScore += amount;
        StartCoroutine(AnimateScore());
    }
    
    public void DecrementScore(int amount)
    {
        score -= amount;
    }

    private IEnumerator AnimateScore()
    {
        while (score < targetScore)
        {
            score += Mathf.CeilToInt((targetScore - score) * Time.deltaTime * scoreSpeed * 3f);
            yield return null;
        }
        score = targetScore;
    }
}
