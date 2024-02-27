using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOptionButton : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameManager gameManager;
    public void OpenMenu()
    {
        startMenu.SetActive(false);
        optionMenu.SetActive(true);
    }
    public void CloseMenu()
    {
        gameManager.SaveSettings();
        startMenu.SetActive(true);
        optionMenu.SetActive(false);
    }
}
