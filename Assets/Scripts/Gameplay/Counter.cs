using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class Counter : MonoBehaviour
{
    public int distance = 0;
    public int coins = 0;
    public int enemy = 0;
    public float time = 0;
    public int total = 0;
    public TMP_Text actualCoinText;
    public TMP_Text actualTimeText;
    public TMP_Text distanceText;
    public TMP_Text coinText;
    public TMP_Text enemyText;
    public TMP_Text timeText;
    public TMP_Text totalText;
    [SerializeField] private GameObject timeObject;
    public AudioSource SoundCoin;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Ground: " );
        if(other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            SoundCoin.Play();
            coins = coins + other.GetComponent<Coin>().coin;
            actualCoinText.text = "Coins: " + coins;
            Debug.Log("Coins: " + coins);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt((time % 3600) / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        if(hours > 0)
        {
            actualTimeText.text = "Time: " + hours + ":" + minutes + ":" + seconds;
        }
        else
        {
            actualTimeText.text = "Time: " + minutes + ":" + seconds;
        }
        coinText.text = coins + "";
    }

    public void AddDistance(int distance)
    {
        enemy = enemy + distance;
    }

    public void AddEnemy()
    {
        enemy++;
    }

    public void UpdateValues()
    {
        actualCoinText.text = "Coins: " + coins;
    }
    public void SetAll()
    {
        timeObject.SetActive(false);
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt((time % 3600) / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        String totalTime;
        if (hours > 0)
        {
            totalTime = hours + ":" + minutes + ":" + seconds;
        }
        else
        {
            totalTime =  minutes + ":" + seconds;
        }
        distance = (int)transform.position.x + 0;
        total = distance + coins + (enemy * 10);

        distanceText.text = distance + "";
        coinText.text = coins + "";
        enemyText.text = enemy + "";
        timeText.text = totalTime;
        totalText.text = total + "";
    }
}
