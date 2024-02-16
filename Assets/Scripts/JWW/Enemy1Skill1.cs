using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Skill1 : MonoBehaviour
{
    public EnemySO enemySO;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthSystem healthSystem = null;
        if (collision.tag != "Player")
            return;
        if (collision.GetComponent<HealthSystem>() != null)
            healthSystem = collision.GetComponent<HealthSystem>();
        healthSystem?.ChangeHealth(-enemySO.skillPower);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
