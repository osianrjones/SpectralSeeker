using System;
using UnityEngine;

[Serializable]
public class LeaderboardEntry
{
    public string username;
    public int score;

    public LeaderboardEntry(string username, int score)
    {
        this.username = username;
        this.score = score;
    }
}
