using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyController enemyController;
    private Collider2D _collider2D;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
        _collider2D = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        collision.gameObject.GetComponent<PlayerHealthSystem>().ChangeHealth(-enemyController.enemySO.power);
    }
    public void EndOfAttack()
    {
        enemyController.isAttacking = false;
    }
}
