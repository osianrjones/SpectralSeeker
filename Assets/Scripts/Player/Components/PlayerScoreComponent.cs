using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreComponent : MonoBehaviour
{
    public int score { get; private set; }

    public static PlayerScoreComponent Instance { get; private set; }
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        score = 0;
    }

    public void IncrementScore(int amount)
    {
        score += amount;
    }
    
    public void DecrementScore(int amount)
    {
        score -= amount;
    }
}
