using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 10.0f;
    public float defence = 0.0f;
    public float damageReduction = 1.0f;
    public float acceleration = 5.0f;
    public float maxSpeed = 5.0f;
    public float maxJumpHeight = 7.0f;
    public float dashSpeed = 10.0f;
    public float immunityTime = 0.5f;
    public int maxJumps = 1;

    public float bulletSpeed = 15.0f;
    public float fireRate = 5.0f;
    public float bulletSize = 1.0f;
    public float bulletDamage = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.maxHealth = maxHealth;
        PlayerController.health = maxHealth;
        PlayerController.defence = defence;
        PlayerController.damageReduction = damageReduction;
        PlayerController.acceleration = acceleration;
        PlayerController.maxSpeed = maxSpeed;
        PlayerController.maxJumpHeight = maxJumpHeight;
        PlayerController.dashSpeed = dashSpeed;
        PlayerController.immunityTime = immunityTime;
        PlayerController.maxJumps = maxJumps;
        Shooter.bulletSpeed = bulletSpeed;
        Shooter.fireRate = fireRate;
        Shooter.bulletSize = bulletSize;
        Shooter.bulletDamage = bulletDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (BothOrNeither.close)
        {
            maxHealth = PlayerController.maxHealth;
            defence = PlayerController.defence;
            damageReduction = PlayerController.damageReduction;
            acceleration = PlayerController.acceleration;
            maxSpeed = PlayerController.maxSpeed;
            maxJumpHeight = PlayerController.maxJumpHeight;
            dashSpeed = PlayerController.dashSpeed;
            immunityTime = PlayerController.immunityTime;
            maxJumps = PlayerController.maxJumps;
            bulletSpeed = Shooter.bulletSpeed;
            fireRate = Shooter.fireRate;
            bulletSize = Shooter.bulletSize;
            bulletDamage = Shooter.bulletDamage;
        }
    }
}
