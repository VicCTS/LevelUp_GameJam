using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenguageGenerator : MonoBehaviour
{
    [SerializeField] private LenguageData _lenguageData = new LenguageData();

    [SerializeField] private string lenguageToCreate;

    public void SaveIntoJson()
    {
        string lenguage = JsonUtility.ToJson(_lenguageData, true);
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/" + lenguageToCreate + ".json", lenguage);
    }
}

[System.Serializable]
public class LenguageData
{
    /*public string[] mainMenuButtons;
    public string[] lenguageButtons;*/
    public string[] textLines;
    public Options[] options;
    public Answers[] answers;
}

[System.Serializable]
public class Answers
{
    public string[] answerLines;
}

[System.Serializable]
public class Options
{
    public string[] optionsLines;
}