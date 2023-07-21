using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get; private set;}

    public enum GameState
    {
        MainMenu,
        Intro,
        Talking,
        Question
    }

    public GameState currentState;

    public static event Action OnIntroStart;
    public static event Action OnGameStart;

    [SerializeField] GameObject _menuCanvas;
    [SerializeField] GameObject _inGameCanvas;
    [SerializeField] GameObject _fadeCanvas;

    [SerializeField] GameObject _menuCamera;
    [SerializeField] GameObject _mainCamera;

    [SerializeField] PlayableDirector _timeLine;

    void OnEnable()
    {
        Answer.OnAnswerSelected += Show_inGameCanvas;
        DialogueManager.OnAnswerWaiting += Hide_inGameCanvas;
        CanvasFade.OnBlackFadeInFinished += StartTimeLine;
    }

    void OnDisable()
    {
        Answer.OnAnswerSelected -= Show_inGameCanvas;
        DialogueManager.OnAnswerWaiting -= Hide_inGameCanvas;
        CanvasFade.OnBlackFadeInFinished += StartTimeLine;
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
        currentState = GameState.MainMenu;
    }

    public void StartIntro()
    {
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/

        currentState = GameState.Intro;

        _fadeCanvas.SetActive(true);

        OnIntroStart();

        _timeLine.Play();
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentState = GameState.Intro;

        _menuCanvas.SetActive(false);
        _fadeCanvas.SetActive(true);

        //Show_inGameCanvas();

        OnGameStart();
    }

    void StartTimeLine()
    {
        _menuCanvas.SetActive(false);
        
        _menuCamera.SetActive(false);
        _mainCamera.SetActive(true);

        
    }

    public void Hide_inGameCanvas()
    {
        _inGameCanvas.SetActive(false);
    }

    public void Show_inGameCanvas()
    {
        _inGameCanvas.SetActive(true);
    }
}
