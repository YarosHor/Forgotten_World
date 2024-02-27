using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject sword1;
    [SerializeField] private GameObject sword2;
    [SerializeField] private GameObject armour1;
    [SerializeField] private GameObject armour2;
    [SerializeField] private GameObject potion;
    public AudioSource soundOpenChest;

    public Animator Anim { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<PlayerInput>().InteractInput)
        {
            Anim.SetBool("Open", true);
        }
    }

    private void Opening()
    {
        soundOpenChest.Play();
        Anim.SetBool("Opened", true);
        int value = Random.Range(0, 4);
        switch (value)
        {
            case 0:
                Instantiate(sword1, (new Vector3(transform.position.x, transform.position.y + 1, 0)), transform.rotation);
                break;
            case 1:
                Instantiate(armour1, (new Vector3(transform.position.x, transform.position.y + 1, 0)), transform.rotation);
                break;
            case 2:
                Instantiate(sword2, (new Vector3(transform.position.x, transform.position.y + 1, 0)), transform.rotation);
                break;
            case 3:
                Instantiate(armour2, (new Vector3(transform.position.x, transform.position.y + 1, 0)), transform.rotation);
                break;
            case 4:
                Instantiate(potion, (new Vector3(transform.position.x, transform.position.y + 1, 0)), transform.rotation);
                break;
            case 5:
                Instantiate(potion, (new Vector3(transform.position.x, transform.position.y + 1, 0)), transform.rotation);
                break;
            case 6:
                Instantiate(potion, (new Vector3(transform.position.x, transform.position.y + 1, 0)), transform.rotation);
                break;
            default:
                break;

        }
    }
}
