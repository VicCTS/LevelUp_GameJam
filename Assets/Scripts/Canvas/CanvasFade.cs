using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CanvasFade : MonoBehaviour
{
    public static event Action OnBlackFadeInFinished;

    [SerializeField] Image _backgroundToFade;

    [SerializeField] bool _whiteFading = false;

    void OnEnable()
    {
        GameManager.OnIntroStart += BlackFade;
    }

    void OnDisable()
    {
        GameManager.OnIntroStart -= BlackFade;
    }

    void Awake()
    {
        _backgroundToFade.canvasRenderer.SetAlpha(0f);
    }

    void Update()
    {
        if(_backgroundToFade.canvasRenderer.GetAlpha() == 1f && _whiteFading)
        {
            Fade(0, 2, Color.white);
            //gameObject.SetActive(false);
        }
        else if(_backgroundToFade.canvasRenderer.GetAlpha() == 1f && !_whiteFading)
        {
            Fade(0, 2, Color.black);

            OnBlackFadeInFinished();
            
            //gameObject.SetActive(false);
        }
    }

    public void WhiteFade()
    {
        _whiteFading = true;

        Fade(1f, 2f, Color.white);
    }

    public void BlackFade()
    {
        Fade(1f, 2f, Color.black);
    }

    void Fade(float alpha, float time, Color fadeColor)
    {
        _backgroundToFade.color = fadeColor;
        _backgroundToFade.CrossFadeAlpha(alpha, time,false);
    }
}
