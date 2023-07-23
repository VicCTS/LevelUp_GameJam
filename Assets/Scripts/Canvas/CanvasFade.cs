using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CanvasFade : MonoBehaviour
{
    public static event Action OnBlackFadeInFinished;
    public static event Action OnShowForest;
    public static event Action OnShowFire;
    public static event Action OnShowSala;
    public static event Action OnReset;
    public static event Action OnFinal;

    [SerializeField] Image _backgroundToFade;

    [SerializeField] bool _whiteFading = false;
    public bool _forest = false;
    bool _fire = false;
    bool _sala = false;
    bool _reset = false;
    bool _final = false;

    void OnEnable()
    {
        GameManager.OnIntroStart += BlackFade;
        DialogueManager.OnGeaAppears += WhiteFade;
        DialogueManager.OnGeaG9 += WhiteFadeForest;
        DialogueManager.OnGeaG11 += BlackFadeFire;
        DialogueManager.OnGeaRO7 += WhiteFadeSala;
        DialogueManager.OnReset += WhiteFadeReset;
        DialogueManager.OnEndGame += WhiteFadeFinal;
    }

    void OnDisable()
    {
        GameManager.OnIntroStart -= BlackFade;
        DialogueManager.OnGeaAppears -= WhiteFade;
        DialogueManager.OnGeaG9 -= WhiteFadeForest;
        DialogueManager.OnGeaG11 -= BlackFadeFire;
        DialogueManager.OnGeaRO7 -= WhiteFadeSala;
        DialogueManager.OnReset -= WhiteFadeReset;
        DialogueManager.OnEndGame -= WhiteFadeFinal;
    }

    void Awake()
    {
        _backgroundToFade.canvasRenderer.SetAlpha(0f);
    }

    void Update()
    {
        if(_backgroundToFade.canvasRenderer.GetAlpha() == 1f && _whiteFading && !_forest && !_fire && !_sala && !_reset && !_final)
        {
            Fade(0, 2, Color.white);
            //gameObject.SetActive(false);
        }
        else if(_backgroundToFade.canvasRenderer.GetAlpha() == 1f && !_whiteFading && !_forest && !_fire && !_sala && !_reset && !_final)
        {
            Fade(0, 2, Color.black);

            OnBlackFadeInFinished();
            
            //gameObject.SetActive(false);
        }
        else if(_backgroundToFade.canvasRenderer.GetAlpha() == 1f && _whiteFading && _forest && !_fire && !_sala && !_reset && !_final)
        {
            Fade(0, 2, Color.white);

            OnShowForest();
        }
        else if(_backgroundToFade.canvasRenderer.GetAlpha() == 1f && _whiteFading && _forest && _fire && !_sala && !_reset && !_final)
        {
            Fade(0, 2, Color.black);

            OnShowFire();
        }
        else if(_backgroundToFade.canvasRenderer.GetAlpha() == 1f && _whiteFading && _forest && _fire && _sala && !_reset && !_final)
        {
            Fade(0, 2, Color.white);

            OnShowSala();
        }
        else if(_backgroundToFade.canvasRenderer.GetAlpha() == 1f && _whiteFading && _forest && _fire && _sala && _reset && !_final)
        {
            Fade(0, 2, Color.white);

            OnReset();
        }
        else if(_backgroundToFade.canvasRenderer.GetAlpha() == 1f && _whiteFading && _forest && _fire && _sala && _reset && _final)
        {
            Fade(0, 2, Color.white);

            OnFinal();
        }
    }

    public void WhiteFade()
    {
        _whiteFading = true;

        Fade(1f, 2f, Color.white);
    }

    public void WhiteFadeForest()
    {
        _forest = true;

        Fade(1f, 2f, Color.white);
    }

    public void WhiteFadeSala()
    {
        _sala = true;

        Fade(1f, 2f, Color.white);
    }

    public void WhiteFadeReset()
    {
        _reset = true;

        Fade(1f, 2f, Color.white);
    }

    public void WhiteFadeFinal()
    {
        _final = true;

        Fade(1f, 2f, Color.white);
    }

    public void BlackFade()
    {
        Fade(1f, 2f, Color.black);
    }

    public void BlackFadeFire()
    {
        _fire = true;
        Fade(1f, 2f, Color.black);
    }

    void Fade(float alpha, float time, Color fadeColor)
    {
        _backgroundToFade.color = fadeColor;
        _backgroundToFade.CrossFadeAlpha(alpha, time,false);
    }
}
