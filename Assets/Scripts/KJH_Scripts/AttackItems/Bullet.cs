using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ### Stats
    public float Atk { get; protected set; } // 공격력    
    public float TravelSpeed { get; protected set; } // 이동속도

    public virtual void Setup(Vector3 spawnPos, Vector2 dir, float atk, float travelSpeed, float duration) 
    {
        Atk = atk;        
        TravelSpeed = travelSpeed;
    }
}
