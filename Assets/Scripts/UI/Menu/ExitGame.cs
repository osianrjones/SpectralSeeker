using UnityEngine;

public class ExitGame : MonoBehaviour
{

    [SerializeField] private GameObject answerSelections;

    private static int score;

    public void FinishGame(bool correctAnswers)
    {
        if (correctAnswers) score += 100;

        Leaderboard.Instance.UpdateScore(score);

        MenuManager.BackToMainMenu();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Time.timeScale = 0f;
            score = collision.GetComponentInChildren<PlayerScoreComponent>().score;
            answerSelections.SetActive(true);
        }
    }
}
