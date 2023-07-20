using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance{get; private set;}

    public static event Action OnAnswerWaiting;

    [SerializeField] string[] textLines;
    [SerializeField] int lineIndex = 0;

    [SerializeField] string[] answersLines0;
    [SerializeField] GameObject[] answers0;

    [SerializeField] string[] answersLines1;
    [SerializeField] GameObject[] answers1;

    

    [SerializeField] float textSpeed;
    [SerializeField] float textWaitTime;


    [SerializeField] Text textUI;

    void OnEnable()
    {
        GameManager.OnGameStart += StartDialogue;
        LenguageManager.OnLenguageChanged += ChangeLenguage;
    }

    void OnDisable()
    {
        GameManager.OnGameStart -= StartDialogue;
        LenguageManager.OnLenguageChanged -= ChangeLenguage;
    }

    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this.gameObject); 
        } 
        else 
        { 
            Instance = this; 
        }
    }

    void Start()
    {
        ChangeLenguage();
    }

    void ChangeLenguage()
    {
        textLines = LenguageManager.Instance.lenguageData.textLines;
        answersLines0 = LenguageManager.Instance.lenguageData.answers[0].answerLines;
        answersLines1 = LenguageManager.Instance.lenguageData.answers[1].answerLines;
    }

    void StartDialogue()
    {
        StartCoroutine("TypeDialogue");
    }

    IEnumerator TypeDialogue()
    {
        foreach (char character in textLines[lineIndex].ToCharArray())
        {
            textUI.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
        
        yield return new WaitForSeconds(textWaitTime);

        textUI.text = string.Empty;

        
        if(lineIndex == 2)
        {
            Debug.Log("Esperando respuesta simple");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers0)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 4)
        {
            Debug.Log("Esperando respuesta multiple");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers1)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex < textLines.Length && GameManager.Instance.currentState == GameManager.GameState.Talking)
        {
            lineIndex++;  
            StartCoroutine("TypeDialogue");
        } 
        else
        {
            Debug.Log("Juego acabado?");
        }
        
    }

    public void StartAnswer(int questionNumber, int answerOption)
    {
        textUI.text = string.Empty;

        switch (questionNumber)
        {
            case 0:
                StartCoroutine(TypeAnswer(answersLines0, answerOption));
            break;
            case 1:
                StartCoroutine(TypeAnswer(answersLines1, answerOption));
            break;
        }
    }


    IEnumerator TypeAnswer(string[] answerText, int answerOption)
    {
        foreach (char character in answerText[answerOption].ToCharArray())
        {
            textUI.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(textWaitTime);

        textUI.text = string.Empty;

        StartCoroutine("TypeDialogue");  
    }
}
