using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private int dmg;
    [SerializeField] private int score = 10;
    public event Action<int, HealthBarController> onCollision;
    // Start is called before the first frame update
    void Start()
    {
        DamageManager.instance.SubscribeFunction(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBarController healthBarController = collision.gameObject.GetComponent<HealthBarController>();
            if (healthBarController != null)
            {
                onCollision?.Invoke(dmg, healthBarController);
                //Destroy(gameObject);
            }
        }
    }
}
