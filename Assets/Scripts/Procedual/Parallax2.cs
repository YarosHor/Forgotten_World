using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax2 : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] bool directionLeft;

    float textureWidth;
    // Start is called before the first frame update
    void Start()
    {
        SetupTexture();
        if(directionLeft) moveSpeed = -moveSpeed;
    }
    void SetupTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        textureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    void Scroll()
    {
        float delta = moveSpeed;
        transform.position = new Vector3(delta, 0, 0);
    }

    void CheckReset()
    {
        if((Mathf.Abs(transform.position.x) - textureWidth) > 0)
        {
            transform.position = new Vector3(0.0f, transform.position.y, transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
        CheckReset();
    }
}
