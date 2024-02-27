using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager.LoadSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
