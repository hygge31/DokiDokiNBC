using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ### Stats
    public float Atk { get; protected set; } // 공격력
    public float FireRate { get; protected set; } // 발사속도
    public float TravelSpeed { get; protected set; } // 이동속도

    public Bullet(float atk, float fireRate, float travelSpeed)
    {
        Atk = atk;
        FireRate = fireRate;
        TravelSpeed = travelSpeed;
    }

    private LayerMask levelCollisionLayer;
    private LayerMask targetLayer;

    public virtual void Setup() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer == (levelCollisionLayer | (1 << collision.gameObject.layer)))
        {
            gameObject.SetActive(false);
        }
        else if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            //체력감소
            HealthSystem healthSystem = collision.GetComponentInParent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-Atk);
            }
            gameObject.SetActive(false);
        }
    }


}
