using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _volumeParameter;
    [SerializeField] private Slider _volumeSlider;

    void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
        _volumeSlider.onValueChanged.AddListener(ChangeVolume); 
    }

    void Start()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat(_volumeParameter, _volumeSlider.maxValue);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _volumeSlider.value);
    }

    public void ChangeVolume(float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * 20);
    }
}
