using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider sliderMaster;
    public Slider sliderMusic;
    public Slider sliderSound;
    public AudioMixer audioMixer;
    [SerializeField] private SaveSettings saveSettings;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Grob1 " + saveSettings.LoadedSettingsData.masterVolume);
        Debug.Log("Grob2 " + saveSettings.LoadedSettingsData.musicVolume);
        Debug.Log("Grob3 " + saveSettings.LoadedSettingsData.SFXVolume);
        sliderMaster.value = saveSettings.LoadedSettingsData.masterVolume;
        sliderMusic.value = saveSettings.LoadedSettingsData.musicVolume;
        sliderSound.value = saveSettings.LoadedSettingsData.SFXVolume;
    }

    void Update()
    {
        float volumeMaster = sliderMaster.value;
        float volumeMusic = sliderMusic.value;
        float volumeSFX = sliderSound.value;
        audioMixer.SetFloat("Master", volumeMaster);
        audioMixer.SetFloat("Music", volumeMusic);
        audioMixer.SetFloat("SFX", volumeSFX);
    }
}
