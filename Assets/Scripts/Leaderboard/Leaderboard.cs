using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Leaderboard : MonoBehaviour, ILeaderboard
{
    private static string leaderboardPath;
    private static LeaderboardData leaderboard;

    public static Leaderboard Instance;

    public static string activeUser;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        leaderboardPath = Application.persistentDataPath + "/leaderboard.json";
        loadLeaderboard();
    }

    public void loadLeaderboard()
    {
        if (File.Exists(leaderboardPath))
        {
            string jsonLeaderboard = File.ReadAllText(leaderboardPath);
            leaderboard = JsonUtility.FromJson<LeaderboardData>(jsonLeaderboard);
        }
        else
        {
            leaderboard = new LeaderboardData();
        }
           
    }

    public void SaveLeaderboard(LeaderboardData leaderboard)
    {
        string jsonLeaderboard = JsonUtility.ToJson(leaderboard, true);
        File.WriteAllText(leaderboardPath, jsonLeaderboard);
    }

    public void UpdateScore(int score, string username)
    {
        if (leaderboard != null)
        {
            var entry = leaderboard.leaderboard.FirstOrDefault(p => p.username == username);
            if (entry != null)
            {
                if (score > entry.score)
                {
                    entry.score = score;
                } 
            } else
            {
                leaderboard.leaderboard.Add(new LeaderboardEntry(username, score));
                    
            }

            leaderboard.leaderboard = leaderboard.leaderboard.OrderByDescending(s => s.score).ToList();
            lastPlayer(username);
            SaveLeaderboard(leaderboard);
        } else
        {
            loadLeaderboard();
        }

    }

    public void UpdateScore(int score)
    {
        string username = activeUser;

        if (leaderboard != null)
        {
            var entry = leaderboard.leaderboard.FirstOrDefault(p => p.username == username);
            if (entry != null)
            {
                if (score > entry.score)
                {
                    entry.score = score;
                }
            }
            else
            {
                leaderboard.leaderboard.Add(new LeaderboardEntry(username, score));

            }

            leaderboard.leaderboard = leaderboard.leaderboard.OrderByDescending(s => s.score).ToList();
            lastPlayer(username);
            SaveLeaderboard(leaderboard);
        }
        else
        {
            loadLeaderboard();
        }

    }

    public static List<LeaderboardEntry> TopScores(int count = 10)
    {
        if (leaderboard != null)
        {
           return leaderboard.leaderboard.OrderByDescending(s => s.score).Take(count).ToList();
        }

        return new List<LeaderboardEntry>();
    }

    public static void setActiveUser(string username)
    {
        activeUser = username;
    }

    public int estimatePosition(int score)
    {

        LeaderboardEntry entry = leaderboard.leaderboard.OrderByDescending(s => s.score).ToList().Find(p => p.score < score);
        int index = leaderboard.leaderboard.IndexOf(entry);

        return index;
    } 

    public int playerScore(string username)
    {
        if (leaderboard == null)
        {
            loadLeaderboard();

            int score = leaderboard.leaderboard.Find(p => p.username == username).score;

            return score;
        } else
        {
            int score = leaderboard.leaderboard.Find(p => p.username == username).score;

            return score == 0 ? -1 : score;
        }
    }

    public void lastPlayer(string player)
    {
        Leaderboard.leaderboard.lastPlayer = player;
    }

    public static string getLastPlayer()
    {
        return Leaderboard.leaderboard.lastPlayer;
    }

    public bool unqiueUsername(string wantedUsername)
    {
        return !Leaderboard.leaderboard.leaderboard.Exists(n => n.username == wantedUsername);
    }
}
