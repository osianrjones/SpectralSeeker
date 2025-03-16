using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreComponent : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int lastScore = 0;
    
    [SerializeField] private PlayerScoreComponent _playerScore;
    
    private int RetrieveScore()
    {
        return _playerScore.score;
    }

    private void Update()
    {
        int score = RetrieveScore();

        if (score != lastScore)
        {
            scoreText.text = score.ToString();
            lastScore = score;
        }
    }
}
