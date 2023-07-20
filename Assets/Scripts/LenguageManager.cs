using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LenguageManager : MonoBehaviour
{
    public static LenguageManager Instance{get; private set;}

    public static event Action OnLenguageChanged;
    public LenguageData lenguageData = new LenguageData();

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

        LoadLenguage("Spanish");
    }

    public void LoadLenguage(string lenguageToLoad)
    {
        string lenguage = File.ReadAllText(Application.streamingAssetsPath + "/" + lenguageToLoad + ".json");
        lenguageData = JsonUtility.FromJson<LenguageData>(lenguage);
    }
}
