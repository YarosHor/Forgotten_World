using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Skin : MonoBehaviour
{
    public AnimatorOverrideController skin1;
    public Rigidbody2D RB { get; private set; }

    private bool collectable;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        collectable = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Terrain"))
        {
            // Item entered the trigger of the ground
            // Stop the item from moving by setting its velocity to zero
            RB.gravityScale = 0;
            RB.velocity = Vector2.zero;
            collectable = true;
        }
        /*if (other.gameObject.name == "Player"){
            other.gameObject.GetComponent<Animator>().runtimeAnimatorController = skin1 as RuntimeAnimatorController;
        }*/
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<PlayerInput>().InteractInput && collectable == true)
        {
            //other.gameObject.GetComponent<Animator>().runtimeAnimatorController = skin1 as RuntimeAnimatorController;
            other.gameObject.GetComponent<Player>().ChangeSkin(gameObject);
            
        }
    }

    public void ChangeValues(Sprite actualSprite, AnimatorOverrideController actualSkin, Vector3 actualPosition)
    {
        GetComponent<SpriteRenderer>().sprite = actualSprite;
        GetComponent<Change_Skin>().skin1 = actualSkin;
        transform.position = new Vector3(actualPosition.x, actualPosition.y +1, actualPosition.z);
        RB.gravityScale = 1;
        collectable = false;
    }
}
