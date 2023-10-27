using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private int numberOfProjectiles = 3; 
    [SerializeField] private float spreadAngle = 30f;
    [SerializeField] private Vector2 movementMap;
    [SerializeField] public CustomInput movementAction = null;
    [SerializeField] public float fireDelay = 0.5f;
    [SerializeField] private bool isFiring = false;
    [SerializeField] private float lastFire = 0.0f;

    private void Awake()
    {
        movementAction = new CustomInput();
        myRBD2 = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        movementAction.Enable();
        movementAction.Game.Movement.performed += OnMovementPerformed;
        movementAction.Game.Movement.canceled += OnMovementCanceled;
        movementAction.Game.Fire.performed += FirePerformed;
        movementAction.Game.Fire.canceled += FireCanceled;
    }

    private void OnDisable()
    {
        movementAction.Disable();
        movementAction.Game.Movement.performed -= OnMovementPerformed;
        movementAction.Game.Movement.canceled -= OnMovementCanceled;
        movementAction.Game.Fire.performed -= FirePerformed;
        movementAction.Game.Fire.canceled -= FireCanceled;
    }
    private void OnMovementPerformed(InputAction.CallbackContext obj)
    {
        movementMap = obj.ReadValue<Vector2>();
    }
    private void OnMovementCanceled(InputAction.CallbackContext obj)
    {
        movementMap = Vector2.zero;
    }
    private void FirePerformed(InputAction.CallbackContext obj)
    {
        isFiring = true;
    }
    private void FireCanceled(InputAction.CallbackContext obj)
    {
        isFiring = false;
    }

    private void Update() {

        Vector3 movementPlayer = new Vector3(movementMap.x, movementMap.y);
        myRBD2.velocity = movementPlayer * velocityModifier;

        Vector2 aimInput = movementAction.Game.Aim.ReadValue<Vector2>();
        Vector3 mouseInput = Camera.main.ScreenToWorldPoint(new Vector3(aimInput.x, aimInput.y, 0f));
        CheckFlip(mouseInput.x);

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

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
        BulletController bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
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
