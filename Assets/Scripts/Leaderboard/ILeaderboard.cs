using UnityEngine;

public interface ILeaderboard
{
    void UpdateScore(int score, string username);

    void loadLeaderboard();

    void SaveLeaderboard(LeaderboardData leaderboard);
}
