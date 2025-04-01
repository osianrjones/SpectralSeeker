using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI usernameInput;

    public void PlayGame()
    {
        if (usernameInput.text.Length > 3)
        {
            Leaderboard.setActiveUser(usernameInput.text);
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameScene");

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public string getUsernameInput()
    {
        if (usernameInput.text.Length > 3)
        {
            return usernameInput.text;
        } else
        {
            return "";
        }
    }

    public TextMeshProUGUI getInput()
    {
        return usernameInput;
    }
}
