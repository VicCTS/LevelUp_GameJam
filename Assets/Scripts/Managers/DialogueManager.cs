using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance{get; private set;}

    public static event Action OnAnswerWaiting;

    [SerializeField] AudioSource voiceAudioSource;
    [SerializeField] AudioClip[] textLinesAudios;
    [SerializeField] AudioClip[] optionLinesAudios;
    [SerializeField] AudioClip[] answerLinesAudios;


    [SerializeField] string[] textLines;
    [SerializeField] int lineIndex = 0;

    [SerializeField] string[] answersLines0;
    [SerializeField] GameObject[] answers0;

    [SerializeField] string[] answersLines1;
    [SerializeField] GameObject[] answers1;

    [SerializeField] string[] answersLines2;
    [SerializeField] GameObject[] answers2;

    [SerializeField] string[] answersLines3;
    [SerializeField] GameObject[] answers3;

    [SerializeField] string[] answersLines4;
    [SerializeField] GameObject[] answers4;

    [SerializeField] string[] answersLines5;
    [SerializeField] GameObject[] answers5;

    [SerializeField] string[] answersLines6;
    [SerializeField] GameObject[] answers6;

    [SerializeField] string[] answersLines7;
    [SerializeField] GameObject[] answers7;

    [SerializeField] string[] answersLines8;
    [SerializeField] GameObject[] answers8;

    [SerializeField] string[] answersLines9;
    [SerializeField] GameObject[] answers9;

    [SerializeField] string[] answersLines10;
    [SerializeField] GameObject[] answers10;

    [SerializeField] string[] answersLines11;
    [SerializeField] GameObject[] answers11;

    [SerializeField] string[] answersLines12;
    [SerializeField] GameObject[] answers12;

    [SerializeField] string[] answersLines13;
    [SerializeField] GameObject[] answers13;

    [SerializeField] string[] answersLines14;
    [SerializeField] GameObject[] answers14;

    [SerializeField] float textSpeed;
    [SerializeField] float textWaitTime;


    [SerializeField] Text textUI;

    void OnEnable()
    {
        CanvasFade.OnBlackFadeInFinished += StartDialogue;
        //GameManager.OnIntroStart += StartDialogue;
        LenguageManager.OnLenguageChanged += ChangeLenguage;
    }

    void OnDisable()
    {
        CanvasFade.OnBlackFadeInFinished -= StartDialogue;
        //GameManager.OnIntroStart -= StartDialogue;
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
        answersLines2 = LenguageManager.Instance.lenguageData.answers[2].answerLines;
        answersLines3 = LenguageManager.Instance.lenguageData.answers[3].answerLines;
        answersLines4 = LenguageManager.Instance.lenguageData.answers[4].answerLines;
        answersLines5 = LenguageManager.Instance.lenguageData.answers[5].answerLines;
        answersLines6 = LenguageManager.Instance.lenguageData.answers[6].answerLines;
        answersLines7 = LenguageManager.Instance.lenguageData.answers[7].answerLines;
        answersLines8 = LenguageManager.Instance.lenguageData.answers[8].answerLines;
        answersLines9 = LenguageManager.Instance.lenguageData.answers[9].answerLines;
        answersLines10 = LenguageManager.Instance.lenguageData.answers[10].answerLines;
        answersLines11 = LenguageManager.Instance.lenguageData.answers[11].answerLines;
        answersLines12 = LenguageManager.Instance.lenguageData.answers[12].answerLines;
        answersLines13 = LenguageManager.Instance.lenguageData.answers[13].answerLines;
        answersLines14 = LenguageManager.Instance.lenguageData.answers[14].answerLines;
    }

    void StartDialogue()
    {
        GameManager.Instance.Show_inGameCanvas();
        StartCoroutine("TypeDialogue");
    }

    IEnumerator TypeDialogue()
    {

        voiceAudioSource.PlayOneShot(textLinesAudios[lineIndex]);

        foreach (char character in textLines[lineIndex].ToCharArray())
        {
            textUI.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
        
        yield return new WaitForSeconds(textWaitTime);

        textUI.text = string.Empty;

        ////////////////////////////////////////////////////////////////////////////////////////////////
        //Aqui tengo que haceer cosas
        /////////////////////////////////////////////////////////////////////////////////////////////
        /*if(lineIndex == 2)
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
        else if(lineIndex < textLines.Length)
        {
            lineIndex++;  
            StartCoroutine("TypeDialogue");
        } 
        else if(lineIndex > textLines.Length)
        {
            Debug.Log("Juego acabado?");
        }*/

        if(lineIndex < textLines.Length)
        {
            lineIndex++;  
            StartCoroutine("TypeDialogue");
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