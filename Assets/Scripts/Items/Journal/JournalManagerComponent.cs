using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalManagerComponent : MonoBehaviour, IItem
{
    [SerializeField] GameObject journalPanel;
    [SerializeField] Text inputField;
    [SerializeField] InputField inputFieldComponent;
    [SerializeField] AudioClip pageFlip;

    private string key = "JournalNotes";
    private bool isOpen = false;

    void Start()
    {
        journalPanel.SetActive(false);

        if (inputFieldComponent != null)
        {
            inputFieldComponent.onValidateInput += ValidateInput;
        }
        else
        {
            Debug.LogError("InputField component not found set!");
        }

        LoadNotes();
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        // Return null/empty char to discard digits
        if (char.IsDigit(addedChar))
        {
            return '\0';
        }

        if (char.IsLetter(addedChar) || char.IsWhiteSpace(addedChar) ||
            addedChar == '.' || addedChar == ',' || addedChar == '!' || addedChar == '?')
        {
            return addedChar;
        }

        return '\0';
    }

    public bool ToggleUse()
    {
        isOpen = !journalPanel.activeSelf;
        journalPanel.SetActive(isOpen);
        ServiceLocator.Get<ISoundService>().PlaySFX(pageFlip);

        if (inputField.text.Contains("1"))
        {
            inputField.text = inputField.text.Replace("1", "");
            SaveNotes();
        }

        return true;
    }

    public void SaveNotes()
    {
        string notes = inputField.text;
        PlayerPrefs.SetString(key, notes);
        PlayerPrefs.Save();
    }

    public void LoadNotes()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedNotes = PlayerPrefs.GetString(key);
            inputField.text = loadedNotes;
        }
    }
}
