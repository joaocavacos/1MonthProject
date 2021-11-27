using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Options : MonoBehaviour
{

    public GameObject settingsMenu;
    
    [Header("Sound/Music Settings")]
    public Slider soundSlider;
    public Slider musicSlider;
    [SerializeField] private string soundVolume = "SoundVolume";
    [SerializeField] private AudioMixer soundMixer;
    [SerializeField] private string musicVolume = "MusicVolume";
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private float multiplier = 30f;


    private void Awake()
    {
        soundSlider.onValueChanged.AddListener(SoundSliderChanged);
        musicSlider.onValueChanged.AddListener(MusicSliderChanged);
    }

    void Start()
    {
        soundSlider.value = PlayerPrefs.GetFloat(soundVolume, soundSlider.value);
        musicSlider.value = PlayerPrefs.GetFloat(musicVolume, musicSlider.value);
    }

    private void SoundSliderChanged(float volume)
    {
        soundMixer.SetFloat(soundVolume, Mathf.Log10(volume) * multiplier);
    }
    
    private void MusicSliderChanged(float volume)
    {
        musicMixer.SetFloat(musicVolume, Mathf.Log10(volume) * multiplier);
    }
    
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(soundVolume, soundSlider.value);
        PlayerPrefs.SetFloat(musicVolume, musicSlider.value);
    }
    
    public void Close()
    {
        settingsMenu.SetActive(false);
    }

    public void Open()
    {
       
        settingsMenu.SetActive(true);
    }
}
