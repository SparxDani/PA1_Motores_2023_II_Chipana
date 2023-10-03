using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovement : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocityModifier;
    public BulletController bullet;
    private Transform currentTarget;
    private bool follow;
    private bool isMoving;
    private bool canShoot = true;

    private void Start()
    {
        currentTarget = transform;
    }

    private void Update()
    {
        if (isMoving)
        {
            rb.velocity = (currentTarget.position - enemyTransform.position).normalized * velocityModifier;
            if (follow && canShoot)
            {
                StartCoroutine(ShootBullet());  
                canShoot = false;
            }
            Distance();
        }
        else
        {
            rb.velocity = (currentTarget.position - enemyTransform.position).normalized * velocityModifier;
            Distance();
        }
    }

    private void Distance()
    {
        if ((currentTarget.position - enemyTransform.position).magnitude < 0.05f)
        {
            enemyTransform.position = currentTarget.position;
            isMoving = false;
            rb.velocity = Vector2.zero;
        }
        else
        {
            isMoving = true;
        }
    }
    IEnumerator ShootBullet()
    {
        Instantiate(bullet, enemyTransform.position, Quaternion.identity).SetUpVelocity(rb.velocity, "Enemy");
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentTarget = other.transform;
            follow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentTarget = transform;
            follow = false;
        }
    }
}
