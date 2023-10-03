using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private int numberOfProjectiles = 3; 
    [SerializeField] private float spreadAngle = 30f;


    private void Update() {
        Vector2 movementPlayer = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        myRBD2.velocity = movementPlayer * velocityModifier;

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckFlip(mouseInput.x);
    
        Debug.DrawRay(transform.position, mouseInput.normalized * rayDistance, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Right Click");
            LaunchMultipleProjectiles();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Left Click");
            LaunchProjectile();
        }
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    private void LaunchProjectile()
    {
        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mouseInput - (Vector2)transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        Destroy(projectile, 2f);
    }
    private void LaunchMultipleProjectiles()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * (spreadAngle / (numberOfProjectiles - 1)) - (spreadAngle / 2f);
            Vector2 direction = Quaternion.Euler(0f, 0f, angle) * (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;

            Destroy(projectile, 2f);
        }
    }
}
