using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovement : MonoBehaviour
{
    [SerializeField] private Transform wizTrans;
    [SerializeField] private Rigidbody2D wizRB;
    [SerializeField] private float velocityModifier;
    private Transform currentTarget;
    private bool follow;
    private bool isMoving;

    private void Start()
    {
        currentTarget = transform;
    }

    private void Update()
    {
        if (isMoving)
        {
            wizRB.velocity = (currentTarget.position - wizTrans.position).normalized * velocityModifier;
            Distance();
        }
        else
        {
            wizRB.velocity = (currentTarget.position - wizTrans.position).normalized * velocityModifier;
            Distance();
        }
    }

    private void Distance()
    {
        if ((currentTarget.position - wizTrans.position).magnitude < 0.05f)
        {
            wizTrans.position = currentTarget.position;
            isMoving = false;
            wizRB.velocity = Vector2.zero;
        }
        else
        {
            isMoving = true;
        }
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
