using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gea : MonoBehaviour
{
    Animator geaAnimator;

    void OnEnable()
    {
        DialogueManager.OnGeaRO4A += RO4A;
        DialogueManager.OnGeaRO4B += RO4B;
        DialogueManager.OnGeaG9 += G9;
        DialogueManager.OnGeaRO5A += RO5A;
        DialogueManager.OnGeaRO5B += RO5B;
        DialogueManager.OnGeaG11 += G11;
        DialogueManager.OnGeaRO6A += RO6A;
        DialogueManager.OnGeaRO6B += RO6B;
        DialogueManager.OnGeaG13 += G13;
        CanvasFade.OnReset += FirstAnim;
    }

    void OnDisable()
    {
        DialogueManager.OnGeaRO4A -= RO4A;
        DialogueManager.OnGeaRO4B -= RO4B;
        DialogueManager.OnGeaG9 -= G9;
        DialogueManager.OnGeaRO5A -= RO5A;
        DialogueManager.OnGeaRO5B -= RO5B;
        DialogueManager.OnGeaG11 -= G11;
        DialogueManager.OnGeaRO6A -= RO6A;
        DialogueManager.OnGeaRO6B -= RO6B;
        DialogueManager.OnGeaG13 -= G13;
        CanvasFade.OnReset -= FirstAnim;
    }

    // Start is called before the first frame update
    void Awake()
    {
        geaAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RO4A()
    {
        geaAnimator.SetTrigger("RO4A");
    }
    void RO4B()
    {
        geaAnimator.SetTrigger("RO4B");
    }
    void G9()
    {
        geaAnimator.SetTrigger("G9");
    }
    void RO5A()
    {
        geaAnimator.SetTrigger("RO5A");
    }
    void RO5B()
    {
        geaAnimator.SetTrigger("RO5B");
    }
    void G11()
    {
        geaAnimator.SetTrigger("G11");
    }
    void RO6A()
    {
        geaAnimator.SetTrigger("RO6A");
    }
    void RO6B()
    {
        geaAnimator.SetTrigger("RO6B");
    }
    void G13()
    {
        geaAnimator.SetTrigger("G13");
    }

    void FirstAnim()
    {
        geaAnimator.SetTrigger("FirstAnim");
    }
}
