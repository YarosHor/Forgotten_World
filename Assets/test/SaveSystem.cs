using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    public string playerHealthKey = "PlayerHealth", distanceKey = "PlayerDistance", coinsKey = "PlayerCoins", enemyKey = "PlayerEnemies", timeKey = "PlayerTime", savePresentKey = "SavePresent";
    public LoadedData LoadedData { get; private set; }

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
        PlayerPrefs.DeleteKey(playerHealthKey);
        PlayerPrefs.DeleteKey(distanceKey);
        PlayerPrefs.DeleteKey(coinsKey);
        PlayerPrefs.DeleteKey(enemyKey);
        PlayerPrefs.DeleteKey(timeKey);
        PlayerPrefs.DeleteKey(savePresentKey);
        LoadedData = null;
    }

    public bool LoadData()
    {

        if (PlayerPrefs.GetInt(savePresentKey) == 1)
        {
            LoadedData = new LoadedData();
            LoadedData.playerHealth = PlayerPrefs.GetInt(playerHealthKey);
            LoadedData.playerDistance = PlayerPrefs.GetInt(distanceKey);
            LoadedData.playerCoins = PlayerPrefs.GetInt(coinsKey);
            LoadedData.playerEnemy = PlayerPrefs.GetInt(enemyKey);
            LoadedData.playerTime = PlayerPrefs.GetInt(timeKey);
            return true;
        }
        return false;

    }

    public void SaveData(int sceneIndex, int playerHealth, int playerDistance, int playerCoins, int playerEnemy, float playerTime)
    {
        if (LoadedData == null)
            LoadedData = new LoadedData();
        LoadedData.playerHealth = playerHealth;
        LoadedData.playerDistance = playerDistance;
        LoadedData.playerCoins = playerCoins;
        LoadedData.playerEnemy = playerEnemy;
        LoadedData.playerTime = playerTime;
        PlayerPrefs.SetInt(playerHealthKey, playerHealth);
        PlayerPrefs.SetInt(distanceKey, playerDistance);
        PlayerPrefs.SetInt(coinsKey, playerCoins);
        PlayerPrefs.SetInt(enemyKey, playerEnemy);
        PlayerPrefs.SetFloat(playerHealthKey, playerTime);
        PlayerPrefs.SetInt(timeKey, 1);
    }

}

public class LoadedData
{
    public int playerHealth = -1;
    public int playerDistance = 0;
    public int playerCoins = 0;
    public int playerEnemy = 0;
    public float playerTime = 0;

}