using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveSettings : MonoBehaviour
{
    public string masterVolumeKey = "masterVolume", musicVolumeKey = "masterVolume", SFXVolumeKey = "masterVolume", savePresentKey = "SavePresent";
    public LoadedSettingsData LoadedSettingsData { get; private set; }

    public UnityEvent<bool> OnDataLoadedResult;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        var result = LoadData();
        OnDataLoadedResult?.Invoke(result);
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteKey(masterVolumeKey);
        PlayerPrefs.DeleteKey(musicVolumeKey);
        PlayerPrefs.DeleteKey(SFXVolumeKey);
        PlayerPrefs.DeleteKey(savePresentKey);
        LoadedSettingsData = null;
    }

    public bool LoadData()
    {

        if (PlayerPrefs.GetInt(savePresentKey) == 1)
        {
            LoadedSettingsData = new LoadedSettingsData();
            LoadedSettingsData.masterVolume = PlayerPrefs.GetFloat(masterVolumeKey);
            LoadedSettingsData.musicVolume = PlayerPrefs.GetFloat(musicVolumeKey);
            LoadedSettingsData.SFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
            return true;
        }
        return false;

    }

    public void SaveData(float masterVolume, float musicVolume, float SFXVolume)
    {
        if (LoadedSettingsData == null)
            LoadedSettingsData = new LoadedSettingsData();
        LoadedSettingsData.masterVolume = masterVolume;
        LoadedSettingsData.musicVolume = musicVolume;
        LoadedSettingsData.SFXVolume = SFXVolume;
        PlayerPrefs.SetFloat(masterVolumeKey, masterVolume);
        PlayerPrefs.SetFloat(musicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, SFXVolume);
    }

}

public class LoadedSettingsData
{
    public float masterVolume = 0;
    public float musicVolume = 0;
    public float SFXVolume = 0;

}