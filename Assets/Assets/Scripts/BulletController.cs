using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRGB2D;
    [SerializeField] private float velocityMultiplier;

    public void SetUpVelocity(Vector2 velocity, string newTag)
    {
        myRGB2D.velocity = velocity * velocityMultiplier;
    }
}
