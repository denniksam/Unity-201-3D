using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider effectsVolumeSlider;
    [SerializeField]
    private Toggle muteAllToggle;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private AudioMixer soundMixer;

    void Start()
    {
        OnMusicVolumeChanged(musicVolumeSlider.value);
        OnEffectsVolumeChanged(effectsVolumeSlider.value);
        LabirinthState.isSoundsMuted = muteAllToggle.isOn;
        if(content.activeInHierarchy)
        {
            ShowMenu();
        }
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(content.activeInHierarchy)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }
    private void ShowMenu()
    {
        content.SetActive(true);
        Time.timeScale = 0f;
        LabirinthState.isPaused = true;
    }
    private void HideMenu()
    {
        content.SetActive(false);
        Time.timeScale = 1.0f;
        LabirinthState.isPaused = false;
    }

    public void OnMusicVolumeChanged(float volume)
    {
        LabirinthState.musicVolume = volume;
        if (!LabirinthState.isSoundsMuted)
        {
            // volume [0..1], dB [-80..+10]
            float dB = -80f + 90f * volume;
            soundMixer.SetFloat("MusicVolume", dB);
        }
    }
    public void OnEffectsVolumeChanged(float volume)
    {
        LabirinthState.effectsVolume = volume;
        if (!LabirinthState.isSoundsMuted)
        {
            float dB = -80f + 90f * volume;
            soundMixer.SetFloat("EffectsVolume", dB);
        }
    }
    public void OnMuteAllChanged(bool value)
    {
        LabirinthState.isSoundsMuted = value;
        if(value)
        {
            soundMixer.SetFloat("EffectsVolume", -80f);
            soundMixer.SetFloat("MusicVolume", -80f);
        }
        else
        {
            OnEffectsVolumeChanged(LabirinthState.effectsVolume);
            OnMusicVolumeChanged(LabirinthState.musicVolume);
        }
    }
    public void OnMenuButtonClick(int value)
    {
        // Debug.Log(value.ToString());
        switch (value)
        {
            case 1:  // Exit
                if (Application.isEditor)
                {
                    EditorApplication.ExitPlaymode();  // Зупиняє гру
                    // EditorApplication.Exit(0);  // Закриває разом з Unity
                }
                else
                {
                    Application.Quit(0);
                }
                break;
            case 2:  // Defaults
                OnMusicVolumeChanged(0.5f);
                OnEffectsVolumeChanged(0.5f);
                OnMuteAllChanged(false);
                break;
            case 3:  // Close
                HideMenu();
                break;
            default:
                Debug.LogError($"Undefined button click detected: value '{value}'");
                break;
        }
    }
}
