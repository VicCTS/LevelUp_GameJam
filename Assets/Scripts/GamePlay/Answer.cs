using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Answer : MonoBehaviour
{
    public static event Action OnAnswerSelected;

    [SerializeField] int questionNumber;
    [SerializeField] string option;
    [SerializeField] int optionNumber;

    [SerializeField] GameObject[] optionObjects;

    [SerializeField] GameObject canvas;
    [SerializeField] Text textUI;

    public bool selectable = true;

    void Start()
    {
        ChangeLenguage();
    }

    void ChangeLenguage()
    {
        option = LenguageManager.Instance.lenguageData.options[questionNumber].optionsLines[optionNumber];
    }

    public void ShowAnswer()
    {
        canvas.SetActive(true);
        textUI.text = option;
    }

    public void HideAnswer()
    {
        canvas.SetActive(false);
    }

    public void AnswerQuestion()
    {
        OnAnswerSelected();

        GameManager.Instance.currentState = GameManager.GameState.Talking;

        DialogueManager.Instance.StartAnswer(questionNumber, optionNumber);

        foreach (GameObject item in optionObjects)
        {
            item.SetActive(false);
        }
    }
}
