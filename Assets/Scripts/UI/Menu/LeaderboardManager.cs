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

    private static Leaderboard leaderboard;

    private void Start()
    {
        LoadLeaderboard();
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
}
