using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    [SerializeField] int questionNumber;
    [SerializeField] string answer;
    [SerializeField] int answerNumber;

    [SerializeField] GameObject[] answerObjects;

    [SerializeField] Text textUI;

    public void ShowAnswer()
    {
        textUI.text = answer;
    }

    public void AnswerQuestion()
    {
        GameManager.Instance.currentState = GameManager.GameState.Talking;

        DialogueManager.Instance.StartAnswer(questionNumber, answerNumber);

        foreach (GameObject item in answerObjects)
        {
            item.SetActive(false);
        }
    }
}
