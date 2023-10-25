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
    public event Action<int, HealthBarController> onCollision;

    public void SetUpVelocity(Vector2 velocity, string newTag)
    {
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
            if (other.GetComponent<HealthBarController>())
            {
                onCollision?.Invoke(damage, other.GetComponent<HealthBarController>());
            }
            Destroy(this.gameObject);
        }
    }
}