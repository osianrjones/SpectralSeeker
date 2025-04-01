using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LeaderboardManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> usernames;
    [SerializeField] private List<GameObject> scores;

    [SerializeField] private GameObject position;
    [SerializeField] private GameObject username;
    [SerializeField] private GameObject score;

    private static Leaderboard leaderboard;
    private static PlayerScoreComponent playerScore;

    private void Start()
    {
        LoadLeaderboard();
        UpdateLastUsername();
        UpdateCurrentPosition();
        playerScore = PlayerScoreComponent.Instance;
        currentUsernamePosition();
    }

    private void UpdateLastUsername()
    {
        string lastPlayer = Leaderboard.getLastPlayer();
        if (lastPlayer != "")
        {
            gameObject.GetComponent<MenuManager>().getInput().text = lastPlayer;
        }
      
    }

    private void Update()
    {
        UpdateCurrentPosition();
    }

    public void LoadLeaderboard()
    {
        leaderboard = Leaderboard.Instance;
        List<LeaderboardEntry> topScore = Leaderboard.TopScores();

        for (int i = 0; i < topScore.Count; i++)
        {
            usernames[i].GetComponent<Text>().text = topScore[i].username;
            scores[i].GetComponent<Text>().text = topScore[i].score.ToString();
        }
        
    }

    private void currentUsernamePosition()
    {
        string lastPlayer = Leaderboard.getLastPlayer();
        
         int score = Leaderboard.Instance.playerScore(lastPlayer);
         int position = leaderboard.estimatePosition(score);

         this.position.GetComponent<Text>().text = position.ToString();
         this.username.GetComponent<Text>().text = Leaderboard.getLastPlayer().ToString();
         this.score.GetComponent<Text>().text = score.ToString();
    }

    private void UpdateCurrentPosition()
    {
        if (PlayerScoreComponent.Instance != null)
        {
            int score = PlayerScoreComponent.Instance.score;
            int position = leaderboard.estimatePosition(score);
            string username = Leaderboard.activeUser;

            this.position.GetComponent<Text>().text = position.ToString();
            this.username.GetComponent<Text>().text = username.ToString();
            this.score.GetComponent<Text>().text = score.ToString();
        } 
    }
}
