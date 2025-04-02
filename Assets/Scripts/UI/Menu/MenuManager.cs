using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI usernameInput;
    [SerializeField] public TextMeshProUGUI errorText;
    [SerializeField] public GameObject leaderboardUI;
    [SerializeField] public GameObject settingsUI;

    public void PlayGame()
    {
        bool unqiueUser = Leaderboard.Instance.unqiueUsername(usernameInput.text);
        if (usernameInput.text.Length > 3 && unqiueUser)
        {
            Leaderboard.setActiveUser(usernameInput.text);
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameScene");

        } else if (usernameInput.text.Length > 3 && !unqiueUser)
        {
            errorText.SetText("! Username is not unique !");
            errorText.gameObject.SetActive(true);
            StartCoroutine(RemoveError());
        } else if (usernameInput.text.Length <= 3)
        {
            errorText.SetText("! Username must be greater than 3 characters !");
            errorText.gameObject.SetActive(true);
            StartCoroutine(RemoveError());
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

    public void ShowSettings()
    {
        leaderboardUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void ShowLeaderboard()
    {
        settingsUI.SetActive(false);
        leaderboardUI.SetActive(true);
    }

    public string GetUsernameInput()
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

    IEnumerator RemoveError(float time = 6f)
    {
        int count = 0;
        while (count < time)
        {
            yield return new WaitForSeconds(1);
            count++;
        }
        errorText.gameObject.SetActive(false);
    }
}
