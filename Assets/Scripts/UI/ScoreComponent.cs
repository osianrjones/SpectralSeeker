using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreComponent : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private PlayerScoreComponent _playerScore;
    [SerializeField] private Text nextHighScore;


    private int lastScore = 0;
    
    private int RetrieveScore()
    {
        return _playerScore.score;
    }

    private void Update()
    {
        Leaderboard.Instance.TempUpdateScore(lastScore);
        int score = RetrieveScore();
        int differenceToNextPosition = Leaderboard.Instance.scoreNeededForNextPosition(Leaderboard.activeUser);

        if (score != lastScore)
        {
            scoreText.text = score.ToString();
            lastScore = score;
        }
        Debug.Log(differenceToNextPosition);
        nextHighScore.text = (differenceToNextPosition - score).ToString();
    }
}
