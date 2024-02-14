using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Skill1 : MonoBehaviour
{
    public EnemySO enemySO;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthSystem healthSystem = null;
        if (collision.tag != "Player")
            return;
        if (collision.GetComponent<PlayerHealthSystem>() != null)
            healthSystem = collision.GetComponent<PlayerHealthSystem>();
        healthSystem?.ChangeHealth(-enemySO.skillPower);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
