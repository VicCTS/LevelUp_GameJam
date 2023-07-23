using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance{get; private set;}

    public static event Action OnAnswerWaiting;

    public static event Action OnGeaAppears;
    public static event Action OnGeaRO4A;
    public static event Action OnGeaRO4B;
    public static event Action OnGeaG9;
    public static event Action OnGeaRO5A;
    public static event Action OnGeaRO5B;
    public static event Action OnGeaG11;
    public static event Action OnGeaRO6A;
    public static event Action OnGeaRO6B;
    public static event Action OnGeaG13;
    public static event Action OnGeaRO7;
    public static event Action OnReset;
    public static event Action OnEndGame;

    [SerializeField] AudioSource voiceAudioSource;
    [SerializeField] AudioClip[] textLinesAudios;
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

    bool opcion10 = false;

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
        StartCoroutine(TypeDialogue(1f));
    }

    IEnumerator TypeDialogue(float waitTimeNextFrase)
    {
        
        if(lineIndex == 36)
        {
            voiceAudioSource.PlayOneShot(textLinesAudios[lineIndex - 1]);

            foreach (char character in textLines[lineIndex - 1].ToCharArray())
            {
                textUI.text += character;
                yield return new WaitForSeconds(textSpeed);
            }
            
            yield return new WaitForSeconds(textWaitTime);

            textUI.text = string.Empty;

            yield return new WaitForSeconds(waitTimeNextFrase);
            }
        else
        {
            voiceAudioSource.PlayOneShot(textLinesAudios[lineIndex]);

            foreach (char character in textLines[lineIndex].ToCharArray())
            {
                textUI.text += character;
                yield return new WaitForSeconds(textSpeed);
            }
            
            yield return new WaitForSeconds(textWaitTime);

            textUI.text = string.Empty;

            yield return new WaitForSeconds(waitTimeNextFrase);
        }
        

        ////////////////////////////////////////////////////////////////////////////////////////////////
        //Aqui tengo que haceer cosas
        ////////////////////////////////////////////////////////////////////////////////////////////
        if(lineIndex == 2)
        {
            lineIndex++;  
            StartCoroutine(TypeDialogue(10f));
        }
        else if(lineIndex == 3)
        {
            lineIndex++;  
            StartCoroutine(TypeDialogue(11f));
        }
        else if(lineIndex == 5)
        {
            //1 Opcion
            Debug.Log("1 Opcion");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers0)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 7)
        {
            //2 Opcion
            Debug.Log("2 Opcion");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers1)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 8)
        {
            //3 Opcion
            Debug.Log("3 Opcion");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers2)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 11)
        {
            //4 Opcion
            Debug.Log("4 Opcion");

            OnGeaAppears();

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers3)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 12)
        {
            OnGeaG9();

            lineIndex++;  
            StartCoroutine(TypeDialogue(0f));
        }
        else if(lineIndex == 13)
        {
            //5 Opcion
            Debug.Log("5 Opcion");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers4)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 14)
        {
            OnGeaG11();

            lineIndex++;  
            StartCoroutine(TypeDialogue(0f));
        }
        else if(lineIndex == 15)
        {
            //6 Opcion
            Debug.Log("6 Opcion");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers5)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 16)
        {
            OnGeaG13();

            lineIndex++;  
            StartCoroutine(TypeDialogue(0f));
        }
        else if(lineIndex == 17)
        {
            //7 Opcion
            Debug.Log("7 Opcion");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers6)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 19)
        {
            //8 Opcion
            Debug.Log("8 Opcion");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers7)
            {
                answer.SetActive(true);
            }

            //lineIndex++;
        }
        else if(lineIndex == 20)
        {
            OnReset();

            lineIndex++;  
            StartCoroutine(TypeDialogue(2));
        }
        else if(lineIndex == 22)
        {
            //////////////////////////////esto es despues del reset

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers10)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 25)
        {
            //////////////////////////////esto es despues del reset

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers11)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 27)
        {
            //////////////////////////////esto es despues del reset

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers12)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 28)
        {
            //////////////////////////////esto es despues del reset

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers13)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 29)
        {
            //////////////////////////////esto es despues del reset

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers14)
            {
                answer.SetActive(true);
            }

            lineIndex++;
        }
        else if(lineIndex == 36)
        {
            //////////////////////////////fin del juego
            OnEndGame();
            
        }
        else if(lineIndex < textLines.Length)
        {
            lineIndex++;  
            StartCoroutine(TypeDialogue(0f));
        } 
        else if(lineIndex > textLines.Length)
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
                StartCoroutine(TypeDialogue(2f));
            break;
            case 1:
                if(answerOption == 0)
                {
                    voiceAudioSource.PlayOneShot(answerLinesAudios[0]);
                }
                StartCoroutine(TypeAnswer(answersLines1, answerOption, questionNumber, 0));
            break;
            case 2:
                StartCoroutine(TypeDialogue(2f));
            break;
            case 3:
                if(answerOption == 0)
                {
                    OnGeaRO4A();
                    voiceAudioSource.PlayOneShot(answerLinesAudios[1]);
                }
                else
                {
                    OnGeaRO4B();
                    voiceAudioSource.PlayOneShot(answerLinesAudios[2]);
                }
                StartCoroutine(TypeAnswer(answersLines3, answerOption, questionNumber, 0));
            break;
            case 4:
                if(answerOption == 0)
                {
                    OnGeaRO5A();
                    voiceAudioSource.PlayOneShot(answerLinesAudios[3]);
                }
                else
                {
                    OnGeaRO5B();
                    voiceAudioSource.PlayOneShot(answerLinesAudios[4]);
                }
                StartCoroutine(TypeAnswer(answersLines4, answerOption, questionNumber,0 ));
            break;
            case 5:
                if(answerOption == 0)
                {
                    OnGeaRO6A();
                    voiceAudioSource.PlayOneShot(answerLinesAudios[5]);
                }
                else
                {
                    OnGeaRO6B();
                    voiceAudioSource.PlayOneShot(answerLinesAudios[6]);
                }
                StartCoroutine(TypeAnswer(answersLines5, answerOption, questionNumber, 2));
            break;
            case 6:
                OnGeaRO7();
                voiceAudioSource.PlayOneShot(answerLinesAudios[7]);
                StartCoroutine(TypeAnswer(answersLines6, answerOption, questionNumber, 0));
            break;
            case 7:
                voiceAudioSource.PlayOneShot(answerLinesAudios[8]);
                StartCoroutine(TypeAnswer(answersLines7, answerOption, questionNumber, 0));
            break;
            case 8:
                voiceAudioSource.PlayOneShot(answerLinesAudios[9]);
                StartCoroutine(TypeAnswer(answersLines8, answerOption, questionNumber, 0));
            break;
            case 9:
                voiceAudioSource.PlayOneShot(answerLinesAudios[10]);
                StartCoroutine(TypeAnswer(answersLines9, answerOption, questionNumber, 0));
            break;
            default:
                StartCoroutine(TypeDialogue(2f));
            break;
        }
    }


    IEnumerator TypeAnswer(string[] answerText, int answerOption,int questionNum, float waitTimeNextFrase)
    {

        foreach (char character in answerText[answerOption].ToCharArray())
        {
            textUI.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(textWaitTime);

        textUI.text = string.Empty;

        yield return new WaitForSeconds(waitTimeNextFrase);
        
        if(questionNum == 7)
        {
            //Esto es la 9?
            Debug.Log("9 Opcion");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers8)
            {
                answer.SetActive(true);
            }
        }
        else if(questionNum == 8)
        {
            //10 Opcion
            Debug.Log("10 Opcion");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers9)
            {
                answer.SetActive(true);
            }

            /*
            if(!opcion10)
            {
                opcion10 = true;
                StartCoroutine(TypeAnswer(answersLines9, 1, 8, 0));
            }
            else
            {
                Debug.Log("se a acabado la segunda respuesta 10");
                lineIndex++;
                StartCoroutine(TypeDialogue(0));
            }*/
            //voiceAudioSource.PlayOneShot(answerLinesAudios[11]);
            //lineIndex++;
            //StartCoroutine(TypeAnswer(answersLines9, 1, 0, 0));
        }
        else if(questionNum == 9)
        {
            //10 Opcion
            /*Debug.Log("11 Opcion?");

            OnAnswerWaiting();

            GameManager.Instance.currentState = GameManager.GameState.Question;

            foreach (GameObject answer in answers10)
            {
                answer.SetActive(true);
            }*/
            voiceAudioSource.PlayOneShot(answerLinesAudios[11]);
            lineIndex++;
            StartCoroutine(TypeAnswer(answersLines9, 1, 20, 0));
        }
        else if(questionNum == 20)
        {
            StartCoroutine(TypeDialogue(3));
        }
        else
        {
            Debug.Log("comienza dialogo normal despues de una respuesta");
            StartCoroutine(TypeDialogue(0));
        }
          
    }
}