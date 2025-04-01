using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
    public string lastPlayer;
}
