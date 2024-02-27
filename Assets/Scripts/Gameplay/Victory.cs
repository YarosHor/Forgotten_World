using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    private AudioSource victorySound;
    public GameManager gameManager;
    private bool levelCompleted = false;
    private void Start()
    {
        victorySound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Player" && !levelCompleted){
            victorySound.Play();
            levelCompleted = true;
            gameManager.SaveData();
            Invoke("CompleteLevel", 2f);
        }
    }
    private void CompleteLevel(){
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
