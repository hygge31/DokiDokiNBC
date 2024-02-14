using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Fireball : Bullet
{
    public Bullet_Fireball(float atk, float fireRate, float travelSpeed) : base(atk, fireRate, travelSpeed)
    {
        atk = 1f;
        fireRate = 1f;
        travelSpeed = 6f;
    }
    PlayerAttackController attackController;

    private void Awake()
    {
        transform.rotation = attackController.rotation;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = attackController.attackDirection * TravelSpeed;
    }
}
