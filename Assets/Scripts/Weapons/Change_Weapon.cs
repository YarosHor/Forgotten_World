using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Weapon : MonoBehaviour
{
    public AnimatorOverrideController skin1;
    public SOMyWeaponData weaponData;
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
                Debug.Log("mamaguebo");
                // Item entered the trigger of the ground
                // Stop the item from moving by setting its velocity to zero
                RB.gravityScale = 0;
                RB.velocity = Vector2.zero;
                collectable = true;
        }
        /*if (other.gameObject.name == "Player"){
            //OffensiveWeapon offensive = other.gameObject.GetComponentInChildren<OffensiveWeapon>();
            //OffensiveWeapon offensive = this.transform.Find("MySword").gameObject.GetComponent<OffensiveWeapon>();
            //offensive.SetWeapon(skin1, weaponData);
            //other.gameObject.GetComponent<Animator>().runtimeAnimatorController = skin1 as RuntimeAnimatorController;

            GameObject topLevelParent = other.gameObject;

            // Find the intermediate GameObject
            Transform intermediateObject = topLevelParent.transform.Find("Weapons");
            Debug.Log("Parent Found");
            if (intermediateObject != null)
            {
                Debug.Log("Intermediate Found");
                // Find the nested GameObject
                Transform nestedObject = intermediateObject.transform.Find("MySword");

                if (nestedObject != null)
                {
                    Debug.Log("Nested Found");
                    // Get the script component from the nested GameObject
                    OffensiveWeapon scriptComponent = nestedObject.GetComponent<OffensiveWeapon>();

                    if (scriptComponent != null)
                    {
                        Debug.Log("Script Found");
                        // Access the script's methods or properties
                        scriptComponent.SetWeapon(skin1, weaponData);
                    }
                }
            }
        }*/
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<PlayerInput>().InteractInput && collectable == true)
        {
            GameObject topLevelParent = other.gameObject;

            // Find the intermediate GameObject
            Transform intermediateObject = topLevelParent.transform.Find("Weapons");
            Debug.Log("Parent Found");
            if (intermediateObject != null)
            {
                Debug.Log("Intermediate Found");
                // Find the nested GameObject
                Transform nestedObject = intermediateObject.transform.Find("MySword");

                if (nestedObject != null)
                {
                    Debug.Log("Nested Found");
                    // Get the script component from the nested GameObject
                    OffensiveWeapon scriptComponent = nestedObject.GetComponent<OffensiveWeapon>();

                    if (scriptComponent != null)
                    {
                        Debug.Log("Script Found");
                        // Access the script's methods or properties
                        scriptComponent.SetWeapon(gameObject);
                        //Destroy(gameObject);
                    }
                }
            }
        }
    }

    public void ChangeValues(Sprite actualSprite, AnimatorOverrideController actualSkin, SOMyWeaponData newWeaponData, Vector3 actualPosition)
    {
        GetComponent<SpriteRenderer>().sprite = actualSprite;
        skin1 = actualSkin;
        weaponData = newWeaponData;
        transform.position = new Vector3(actualPosition.x, actualPosition.y + 1, actualPosition.z);
        RB.gravityScale = 1;
        collectable = false;
    }
}
