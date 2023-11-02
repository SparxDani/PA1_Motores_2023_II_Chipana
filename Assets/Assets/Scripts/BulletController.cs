using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocityMultiplier;
    [SerializeField] private int damage;
    public AudioClip shootSound;
    private AudioSource audioSource;

    public event Action<int, HealthBarController> onCollision;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null )
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
    public void SetUpVelocity(Vector2 velocity, string newTag)
    {
        DamageManager.instance.SubscribeFunction(this);

        rb.velocity = velocity * velocityMultiplier;
        gameObject.tag = newTag;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(gameObject.tag) && (other.CompareTag("Player") || other.CompareTag("Enemy")))
        {
            if (other.TryGetComponent(out HealthBarController healthBar))
            {
                onCollision?.Invoke(damage, healthBar);
            }
            //Destroy(this.gameObject);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        int damageTaken = CalculateDamageAmount();
        HealthBarController healthBarController = GetHealthBarControllerFromCollision(collision);
        onCollision?.Invoke(damageTaken, healthBarController);


    }*/
}