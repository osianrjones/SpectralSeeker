using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void LoadLeaderboard()
    {
        //todo
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
