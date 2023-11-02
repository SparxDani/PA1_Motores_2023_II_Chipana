using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance {  get; private set; }
  

    //public delegate void DamageCalculationDelegate(int damageTaken, HealthBarController healthBarController);
    //public event DamageCalculationDelegate onDamageCalculation;


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
    public void SubscribeFunction(BulletController enemy)
    {
        enemy.onCollision += CalculateDamage;
    }

    public void SubscribeFunction(EnemyController enemy)
    {
        enemy.onCollision += CalculateDamage;
    }

    public void CalculateDamage(int damageTaken, HealthBarController healthBarController)
    {
        healthBarController.UpdateHealth(-damageTaken);
    }



}
