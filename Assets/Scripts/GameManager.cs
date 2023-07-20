using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public static event Action OnStartGame;

    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject inGameCanvas;

    void OnEnable()
    {
        Answer.OnAnswerSelected += ShowInGameCanvas;
        DialogueManager.OnAnswerWaiting += HideInGameCanvas;
    }

    void OnDisable()
    {
        Answer.OnAnswerSelected -= ShowInGameCanvas;
        DialogueManager.OnAnswerWaiting -= HideInGameCanvas;
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

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentState = GameState.Talking;

        menuCanvas.SetActive(false);
        ShowInGameCanvas();

        OnStartGame();
    }

    public void HideInGameCanvas()
    {
        inGameCanvas.SetActive(false);
    }

    public void ShowInGameCanvas()
    {
        inGameCanvas.SetActive(true);
    }
}
