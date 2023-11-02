using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private int numberOfProjectiles = 3; 
    [SerializeField] private float spreadAngle = 30f;
    [SerializeField] private Vector2 movementMap;
    [SerializeField] private Vector3 mouseInput;
    [SerializeField] public CustomInput movementAction = null;

    
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
        //movementAction.Game.Fire.performed += FirePerformed;
        //movementAction.Game.Fire.canceled += FireCanceled;
    }

    private void OnDisable()
    {
        movementAction.Disable();
        movementAction.Game.Movement.performed -= OnMovementPerformed;
        movementAction.Game.Movement.canceled -= OnMovementCanceled;
        //movementAction.Game.Fire.performed -= FirePerformed;
        //movementAction.Game.Fire.canceled -= FireCanceled;
    }
    private void OnMovementPerformed(InputAction.CallbackContext obj)
    {
        movementMap = obj.ReadValue<Vector2>();
    }
    private void OnMovementCanceled(InputAction.CallbackContext obj)
    {
        movementMap = Vector2.zero;
    }
    /*private void FirePerformed(InputAction.CallbackContext obj)
    {

        isFiring = true;
    }
    private void FireCanceled(InputAction.CallbackContext obj)
    {
        isFiring = false;
    }
    
    private bool CanFire()
    {
        return Time.time >= lastFire + fireDelay;
    }
    private void Fire()
    {
        lastFire = Time.time;
        
    }*/



    private void Update() {

        Vector3 movementPlayer = new Vector3(movementMap.x, movementMap.y);
        myRBD2.velocity = movementPlayer * velocityModifier;

        Vector2 aimInput = movementAction.Game.Aim.ReadValue<Vector2>();
        Vector3 mouseInput = Camera.main.ScreenToWorldPoint(new Vector3(aimInput.x, aimInput.y, 0f));
        CheckFlip(mouseInput.x);
        Vector3 distance = mouseInput - transform.position;

        if (movementAction.Game.FireSingle.triggered)
        {
            BulletController bulletController = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulletController.SetUpVelocity(distance.normalized, gameObject.tag);
        }

        if (movementAction.Game.FireTriple.triggered)
        {
            for (int i = 0; i < numberOfProjectiles; i++) 
            {
                float angle = -spreadAngle / 2 + (spreadAngle / (numberOfProjectiles - 1)) * i;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Vector3 spreadDirection = rotation * distance.normalized;
                BulletController bulletController = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bulletController.SetUpVelocity(spreadDirection, gameObject.tag);
            }
        }

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        CheckFlip(mouseInput.x);

        Debug.DrawRay(transform.position, mouseInput.normalized * rayDistance, Color.red);
        /*if (isFiring && CanFire())
        {
            Fire();
        }*/

    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    
}
