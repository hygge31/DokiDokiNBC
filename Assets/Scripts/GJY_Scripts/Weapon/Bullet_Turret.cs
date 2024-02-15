using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Turret : Bullet
{
    public override void Setup(Vector3 spawnPos, Vector2 dir, float atk, float travelSpeed, float duration)
    {
        base.Setup(spawnPos, dir, atk, travelSpeed, duration);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
