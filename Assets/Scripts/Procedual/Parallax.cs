using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private float lenght;
    [SerializeField] float parallaxEffectX;
    [SerializeField] float parallaxEffectY;
    [SerializeField] Camera cam;
    void Start()
    {
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosX = transform.position.x;
        startPosY = transform.position.y -7;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1- parallaxEffectX));
        float distanceX = (cam.transform.position.x * parallaxEffectX);
        float distanceY = (cam.transform.position.y * parallaxEffectY);
        //transform.position = new Vector3(startPos + distanceX, transform.position.y, transform.position.z);
        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);

        if (temp > startPosX + lenght)
        {
            startPosX += lenght;
        } else if(temp < startPosX - lenght) 
        {
            startPosX -= lenght;
        }
    }
}
