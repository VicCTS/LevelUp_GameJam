using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    private const string _masterParameter = "MasterVolume";
    private const string _musicParameter = "MusicVolume";
    private const string _sfxParameter = "MasterVolume";

    // Start is called before the first frame update
    void Start()
    {
        LoadVolumeSettings();   
    }

    void LoadVolumeSettings()
    {
        float masterVolume =  PlayerPrefs.GetFloat(_masterParameter, 1f);
        float musicVolume =  PlayerPrefs.GetFloat(_musicParameter, 1f);
        float sfxVolume =  PlayerPrefs.GetFloat(_sfxParameter, 1f);

        _audioMixer.SetFloat(_masterParameter, Mathf.Log10(masterVolume)* 20);
        _audioMixer.SetFloat(_musicParameter, Mathf.Log10(musicVolume)* 20);
        _audioMixer.SetFloat(_sfxParameter, Mathf.Log10(sfxVolume)* 20);
    }
}
