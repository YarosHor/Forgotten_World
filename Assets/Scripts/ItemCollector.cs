using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int coins = 0;
    public TMP_Text coinText;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Ground: " );
        if(other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            coinText.text = "Coins: " + coins;
            //Debug.Log("Coins: " + coins);
        }
    }
}
