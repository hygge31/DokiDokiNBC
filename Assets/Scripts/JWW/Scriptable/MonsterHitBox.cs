using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitBox : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponentInParent<HealthSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        if (healthSystem == null)
            return;
        //AttackSO attackSO = collision.gameObject.GetComponent<AttackSO>();
        //if (attackSO == null)
        //    return;
        //healthSystem.ChangeHealth(-attackSO.damage);
        healthSystem.ChangeHealth(-5);//임시
    }
}
