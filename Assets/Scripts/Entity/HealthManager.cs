using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int currentHealth = 0;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject bronzeCoin;
    [SerializeField] private GameObject silverCoin;
    [SerializeField] private GameObject goldCoin;
    [SerializeField] private GameObject canvas;
    public AudioSource soundPlayerHurt, soundPlayerDie, soundEnemyHurt, soundEnemyDie;

    [SerializeField] private bool player;

    private void Start()
    {
        //bronzeCoin = (GameObject)Resources.Load("Prefabs/Coins/BronzeCoin", typeof(GameObject));
        if(currentHealth == 0)
        {
            currentHealth = maxHealth;
        }
        //healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    public void SetHealth(int newMaxHealt, int newCurrentHealt)
    {
        maxHealth = newMaxHealt;
        currentHealth = newCurrentHealt;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        // Update the health bar
        healthBar.SetHealth(currentHealth, maxHealth);
        Debug.Log("artur" + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if(player){
                soundPlayerHurt.Play();
            }
            else
            {
                soundEnemyHurt.Play();
            }
        }
    }

    private void Die()
    {
        
        if (player)
        {
            soundPlayerDie.Play();
            Counter counter = GetComponent<Counter>();
            Destroy(gameObject);
            //canvas.transform.position = new Vector3(0, 700, 0);

            /*distanceText.text = coins;
            coinText.text = coins;
            enemyText.text = coins;
            timeText.text = coins;
            totalText.text = coins;*/
            canvas.SetActive(true);
            canvas.GetComponent<Animator>().SetBool("done", true);

            /*for (int i = 700; i  > 0; i--)
            {
                canvas.transform.position = new Vector3(0, i, 0);
            }*/
            counter.SetAll();
        }
        else
        {
            soundEnemyDie.Play();
            Counter counter = GameObject.Find("Player").GetComponent<Counter>();
            int total = Random.Range(2, 6);
            for (int i = 0; i < total; i++)
            {
                if(Random.Range(1, 10) == 2)
                {
                    Instantiate(silverCoin, (transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 0, 0)), transform.rotation);
                }
                Instantiate(bronzeCoin, (transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 0, 0)), transform.rotation);
            }
            Destroy(gameObject);
            counter.AddEnemy();
        }
        
        //bronzeCoin.SetActive(false);
        //GameObject projectile = Instantiate(bronzeCoin, new Vector3(), new Quaternion());
        
    }
}
