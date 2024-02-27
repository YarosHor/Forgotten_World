using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour
{
    public GameManager GameManager;
    public void Quit()
    {
        GameManager.LoadSound();
        //SceneManager.LoadScene(1);
        //Application.Quit();
    }
}
