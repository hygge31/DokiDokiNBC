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

    public virtual void Setup() { }
}
