using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f;
    public AudioSource soundProjectile;

    private Vector2 direction;
    private float timer;

    private void Start()
    {
        timer = lifetime;
        soundProjectile.Play();
    }

    private void Update()
    {
        direction = new Vector2(1, 0);
        // Move the object based on the stored direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Countdown the timer
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            // Destroy the object after the specified lifetime
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
    }

    // Method to set the direction of the object when it is shot
    public void SetDirection(Vector2 shotDirection)
    {
        //direction = shotDirection.normalized;
        direction = new Vector2(1,0);
    }
}