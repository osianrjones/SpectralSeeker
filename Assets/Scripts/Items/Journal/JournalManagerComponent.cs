using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalManagerComponent : MonoBehaviour, IItem
{
    [SerializeField] GameObject journalPanel;
    [SerializeField] Text inputField;

    private string key = "JournalNotes";

    private bool isOpen = false;
    void Start()
    {
        journalPanel.SetActive(false);
        LoadNotes();
    }

    public bool ToggleUse()
    {
        bool isOpen = journalPanel.activeSelf;
        journalPanel.SetActive(!isOpen);
        if (!isOpen)
        {
            if (inputField.text.Length > 0)
            {
                if (char.IsDigit(inputField.text[inputField.text.Length - 1]))
                {
                    inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
                }
            }
        }
        return true;
    }

    public void SaveNotes()
    {
        string notes = string.Join("\n", inputField.text);
        PlayerPrefs.SetString(key, notes);
        PlayerPrefs.Save();
    }

    public void LoadNotes()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedNotes = PlayerPrefs.GetString(key);
            inputField.text = loadedNotes.Split('\n').ToString();
        }
    }

}
