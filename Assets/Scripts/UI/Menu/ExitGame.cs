using UnityEngine;

public class ExitGame : MonoBehaviour
{

    [SerializeField] private GameObject answerSelection;

    private static int score;

    public void FinishGame(bool correctAnswers)
    {
        if (correctAnswers) score += 100;

        //todo leaderboard

        MenuManager.BackToMainMenu();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Time.timeScale = 0f;
            score = collision.GetComponentInChildren<PlayerScoreComponent>().score;
            answerSelection.SetActive(true);
        }
    }
}
