using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject masterSlider;
    public GameObject musicSlider;
    public GameObject SFXSlider;
    public SaveSystem saveSystem;
    public SaveSettings saveSettings;
    public AudioMixer audioMixer;

    private void Awake()
    {
        SceneManager.sceneLoaded += Initialize;
        DontDestroyOnLoad(gameObject);
    }

    private void Initialize(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("Loaded GM");
        var playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
            player = playerInput.gameObject;
        saveSystem = FindObjectOfType<SaveSystem>();
        if (player != null && saveSystem.LoadedData != null)
        {
            var health = player.GetComponent<HealthManager>();
            var count = player.GetComponent<Counter>();
            health.currentHealth = saveSystem.LoadedData.playerHealth;
            count.distance = saveSystem.LoadedData.playerDistance;
            count.coins = saveSystem.LoadedData.playerCoins;
            count.enemy = saveSystem.LoadedData.playerEnemy;
            count.time = saveSystem.LoadedData.playerTime;
            count.UpdateValues();
        }
        saveSettings.LoadData();
        Debug.Log("Babogus" + saveSettings.LoadedSettingsData.masterVolume);
        if (masterSlider != null && musicSlider != null && SFXSlider != null && saveSettings.LoadedSettingsData != null)
        {
            /*var master = masterSlider.GetComponent<Slider>();
            var music = musicSlider.GetComponent<Slider>();
            var SFX = SFXSlider.GetComponent<Slider>();
            master.value = saveSettings.LoadedSettingsData.masterVolume;
            music.value = saveSettings.LoadedSettingsData.musicVolume;
            SFX.value = saveSettings.LoadedSettingsData.SFXVolume;*/
            /*Debug.Log("Babogusi" +  saveSettings.LoadedSettingsData.masterVolume);
            audioMixer.SetFloat("Master", saveSettings.LoadedSettingsData.masterVolume);
            audioMixer.SetFloat("Music", saveSettings.LoadedSettingsData.musicVolume);
            audioMixer.SetFloat("SFX", saveSettings.LoadedSettingsData.SFXVolume);*/
        }
        else if(saveSettings.LoadedSettingsData != null)
        {
            /*audioMixer.SetFloat("Master", saveSettings.LoadedSettingsData.masterVolume);
            audioMixer.SetFloat("Music", saveSettings.LoadedSettingsData.musicVolume);
            audioMixer.SetFloat("SFX", saveSettings.LoadedSettingsData.SFXVolume);*/
        }
        audioMixer.SetFloat("Master", saveSettings.LoadedSettingsData.masterVolume);
        audioMixer.SetFloat("Music", saveSettings.LoadedSettingsData.musicVolume);
        audioMixer.SetFloat("SFX", saveSettings.LoadedSettingsData.SFXVolume);
        //SceneManager.LoadScene(1);
    }

    public void LoadSound()
    {
        audioMixer.SetFloat("Master", saveSettings.LoadedSettingsData.masterVolume);
        audioMixer.SetFloat("Music", saveSettings.LoadedSettingsData.musicVolume);
        audioMixer.SetFloat("SFX", saveSettings.LoadedSettingsData.SFXVolume);
    }

    public void LoadSettings()
    {
        if (masterSlider != null && musicSlider != null && SFXSlider != null && saveSettings.LoadedSettingsData != null)
        {
            var master = masterSlider.GetComponent<Slider>();
            var music = musicSlider.GetComponent<Slider>();
            var SFX = SFXSlider.GetComponent<Slider>();
            master.value = saveSettings.LoadedSettingsData.masterVolume;
            music.value = saveSettings.LoadedSettingsData.musicVolume;
            SFX.value = saveSettings.LoadedSettingsData.SFXVolume;
            /*master.UpdateValues();
            music.UpdateValues();
            SFX.UpdateValues();*/
        }
    }

    public void LoadLeve()
    {
        if (saveSystem.LoadedData != null)
        {
            //SceneManager.LoadScene(saveSystem.LoadedData.sceneIndex);
            return;
        }
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SaveData()
    {
        if (player != null)
            saveSystem.SaveData(SceneManager.GetActiveScene().buildIndex + 1, player.GetComponent<HealthManager>().currentHealth, player.GetComponent<Counter>().distance, player.GetComponent<Counter>().coins, player.GetComponent<Counter>().enemy, player.GetComponent<Counter>().time);
    }

    public void SaveSettings()
    {
        if (masterSlider != null && musicSlider != null && SFXSlider != null)
            saveSettings.SaveData(masterSlider.GetComponent<Slider>().value, musicSlider.GetComponent<Slider>().value, SFXSlider.GetComponent<Slider>().value);
    }
}