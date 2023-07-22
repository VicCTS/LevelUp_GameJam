using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class LenguageManager : MonoBehaviour
{
    public static LenguageManager Instance{get; private set;}

    public static event Action OnLenguageChanged;
    public LenguageData lenguageData = new LenguageData();

    public Text[] mainMenuButtonsText;
    public Text[] optionsMenuTexts;
    public Text[] creditsMenuTexts;

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

        //LoadLenguage("Spanish");

        string lenguage = File.ReadAllText(Application.streamingAssetsPath + "/Spanish.json");
        lenguageData = JsonUtility.FromJson<LenguageData>(lenguage);
    }

    void Start()
    {
        LoadLenguage("Spanish");
    }

    public void LoadLenguage(string lenguageToLoad)
    {
        string lenguage = File.ReadAllText(Application.streamingAssetsPath + "/" + lenguageToLoad + ".json");
        lenguageData = JsonUtility.FromJson<LenguageData>(lenguage);

        for (int i = 0; i < lenguageData.mainMenuButtons.Length; i++)
        {
            mainMenuButtonsText[i].text = lenguageData.mainMenuButtons[i];
        }

        for (int i = 0; i < lenguageData.optionMenuButtons.Length; i++)
        {
            optionsMenuTexts[i].text = lenguageData.optionMenuButtons[i];
        }

        OnLenguageChanged();
    }
}