using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get; private set;}

    public enum GameState
    {
        MainMenu,
        Intro,
        Talking,
        Question,
        Pause
    }

    public GameState currentState;

    public static event Action OnIntroStart;
    public static event Action OnGameStart;

    [SerializeField] GameObject _menuCanvas;
    [SerializeField] GameObject _inGameCanvas;
    [SerializeField] GameObject _fadeCanvas;
    [SerializeField] GameObject _inGameOptionsCanvas;
    [SerializeField] GameObject _resetMenu;

    [SerializeField] GameObject _menuCamera;
    [SerializeField] GameObject _mainCamera;

    [SerializeField] PlayableDirector _timeLine;
    [SerializeField] PlayableDirector _resetTimeLine;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioClip salaMusica;
    [SerializeField] AudioClip bosqueMusica;
    [SerializeField] AudioClip manoMusica;
    [SerializeField] AudioClip geaApareceEfecto;
    [SerializeField] AudioClip cambioEscenaEfecto;

    [SerializeField] GameObject gea;
    [SerializeField] GameObject sala;
    [SerializeField] GameObject bosque;
    [SerializeField] GameObject mano;

    [SerializeField] GameObject[] fires;
    [SerializeField] GameObject rain;

    [SerializeField] GameObject[] mesaETC;

    [SerializeField] Material skyboxFinal;

    void OnEnable()
    {
        Answer.OnAnswerSelected += Show_inGameCanvas;
        DialogueManager.OnAnswerWaiting += Hide_inGameCanvas;
        CanvasFade.OnBlackFadeInFinished += StartTimeLine;
        DialogueManager.OnGeaAppears += ShowGea;
        CanvasFade.OnShowForest += ActivateForest;
        CanvasFade.OnShowFire += ActivateFire;
        DialogueManager.OnGeaG13 += ActivateRain;
        CanvasFade.OnShowSala += ActivateSala;
        CanvasFade.OnReset += StartResetTimeLine;
        CanvasFade.OnFinal += ActivarFinal;
    }

    void OnDisable()
    {
        Answer.OnAnswerSelected -= Show_inGameCanvas;
        DialogueManager.OnAnswerWaiting -= Hide_inGameCanvas;
        CanvasFade.OnBlackFadeInFinished += StartTimeLine;
        DialogueManager.OnGeaAppears -= ShowGea;
        CanvasFade.OnShowForest -= ActivateForest;
        CanvasFade.OnShowFire -= ActivateFire;
        DialogueManager.OnGeaG13 -= ActivateRain;
        CanvasFade.OnShowSala -= ActivateSala;
        CanvasFade.OnReset -= StartResetTimeLine;
        CanvasFade.OnFinal -= ActivarFinal;
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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.Question)
        {
            _inGameOptionsCanvas.SetActive(true);
            currentState = GameState.Pause;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.Pause)
        {
            CloseOptionsIngame();
        }
    }

    public void CloseOptionsIngame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentState = GameState.Question;
        _inGameOptionsCanvas.SetActive(false);
    }

    public void StartIntro()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentState = GameState.Intro;

        _fadeCanvas.SetActive(true);

        OnIntroStart();

        _timeLine.Play();

        //Show_inGameCanvas();
    }

    public void StartGame()
    {
        currentState = GameState.Talking;

        _menuCanvas.SetActive(false);
        _fadeCanvas.SetActive(true);

        OnGameStart();
    }

    void StartTimeLine()
    {
        _menuCanvas.SetActive(false);
        
        _menuCamera.SetActive(false);
        _mainCamera.SetActive(true);        
    }

    void StartResetTimeLine()
    {
        sfxSource.PlayOneShot(cambioEscenaEfecto);
        _resetTimeLine.Play();       
    }

    public void Hide_inGameCanvas()
    {
        _inGameCanvas.SetActive(false);
    }

    public void Show_inGameCanvas()
    {
        _inGameCanvas.SetActive(true);
    }

    public void ShowGea()
    {
        gea.SetActive(true);
        musicSource.clip = salaMusica;
        musicSource.Play();
        sfxSource.PlayOneShot(geaApareceEfecto);
    }
    public void HideGea()
    {
        gea.SetActive(false);
    }

    void ActivateForest()
    {
        gea.SetActive(true);
        sala.SetActive(false);
        bosque.SetActive(true);
        musicSource.clip = bosqueMusica;
        musicSource.Play();
        sfxSource.PlayOneShot(cambioEscenaEfecto);
    }

    void ActivateFire()
    {
        foreach (GameObject item in fires)
        {
            item.SetActive(true);
        }
    }

    void ActivateRain()
    {
        rain.SetActive(true);
    }

    void ActivateSala()
    {
        sala.SetActive(true);
        bosque.SetActive(false);
        musicSource.clip = salaMusica;
        musicSource.Play();
        sfxSource.PlayOneShot(cambioEscenaEfecto);
    }

    void ActivarFinal()
    {
        RenderSettings.skybox = skyboxFinal;

        gea.SetActive(false);
        sala.SetActive(false);
        bosque.SetActive(false);
        Hide_inGameCanvas();
        foreach (GameObject item in mesaETC)
        {
            item.SetActive(false);
        }
        mano.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _resetMenu.SetActive(true);

        musicSource.clip = manoMusica;
        musicSource.Play();
    }

    public void ReloadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
