using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class AnswerSelection : MonoBehaviour
{
    private bool firstRowSelected = false;
    private bool secondRowSelected = false;
    private bool thirdRowSelected = false;

    [SerializeField] private string firstAnswer;
    [SerializeField] private string secondAnswer;
    [SerializeField] private string thirdAnswer;

    [SerializeField] private List<Button> firstRowButtons;
    [SerializeField] private List<Button> secondRowButtons;
    [SerializeField] private List<Button> thirdRowButtons;

    [SerializeField] private ExitGame gameManager;

    public void firstRow(Button button)
    {
        this.firstRowSelected = true;
        checkAnswer(1, button);

        foreach (Button b in firstRowButtons)
        {
            b.interactable = false;
        }

    }
    public void secondRow(Button button)
    {
        this.secondRowSelected = true;
        checkAnswer(2, button);

        foreach (Button b in secondRowButtons)
        {
            b.interactable = false;
        }
    }
    public void thirdRow(Button button)
    {
        this.thirdRowSelected = true;
        checkAnswer(3, button);

        foreach (Button b in thirdRowButtons)
        {
            b.interactable = false;
        }
    }

    private void checkAnswer(int row, Button button)
    {
        string buttonValue = button.GetComponentInChildren<TMP_Text>().text;
        Debug.Log("Row " + row + " : " + buttonValue);
        bool correct = false;
        switch (row)
        {
            case 1:
                correct = buttonValue == firstAnswer;
                break;
            case 2:
                correct = buttonValue == secondAnswer;
                break;
            case 3:
                correct = buttonValue == thirdAnswer;
                break;
            default:
                break;
        }
        
        if (allSelections())
        {
            gameManager.FinishGame(correct);
        }
    }

    private bool allSelections()
    {
        return firstRowSelected && secondRowSelected && thirdRowSelected;
    }

}
