using UnityEngine;

public class ExitGame : MonoBehaviour
{

    [SerializeField] private GameObject answerSelection;
    [SerializeField] private MenuManager menuManager;

    private static int score;

    public void FinishGame(bool correctAnswers)
    {
        if (correctAnswers) score += 100;

        //todo leaderboard

        menuManager.BackToMainMenu();
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
