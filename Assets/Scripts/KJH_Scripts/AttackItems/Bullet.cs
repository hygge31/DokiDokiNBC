using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected LayerMask targetLayer;

    // ### Stats
    public float Atk { get; protected set; } // 공격력    
    public float TravelSpeed { get; protected set; } // 이동속도
    public float Duration { get; protected set; } // 지속시간
    public int PierceCount { get; protected set; } // 관통 수

    public virtual void Setup(Vector3 spawnPos, Vector2 dir, float atk, float travelSpeed, float duration, int pierceCount)
    {
        Atk = atk;
        TravelSpeed = travelSpeed;
        Duration = duration;
        PierceCount = pierceCount;
    }
}
