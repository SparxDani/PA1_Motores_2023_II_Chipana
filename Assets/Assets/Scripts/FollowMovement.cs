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
            MoveTowardsTarget();
            if (follow && canShoot)
            {
                StartCoroutine(ShootBullet());
                canShoot = false;
            }
            CheckDistance();
        }
        else
        {
            MoveTowardsTarget();
            CheckDistance();
        }
    }

    private void MoveTowardsTarget()
    {
        rb.velocity = (currentTarget.position - transform.position).normalized * velocityModifier;
    }

    private void CheckDistance()
    {
        if ((currentTarget.position - transform.position).magnitude < 0.05f)
        {
            transform.position = currentTarget.position;
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
        Instantiate(bullet, transform.position, Quaternion.identity).SetUpVelocity(rb.velocity, "Enemy");
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
