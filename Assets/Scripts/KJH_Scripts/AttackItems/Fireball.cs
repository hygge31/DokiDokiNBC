using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Fireball : MonoBehaviour, IAttack
{
    private CharacterStatsHandler charStats;

    [Header("Base Stats")]
    private float projectileSpeed = 10.0f; // 투사체 속도
    private float atk = 1f; //투사체 데미지
    private float fireRate = 1f; // 발사속도

    private GameObject player; // 플레이어 할당

    [SerializeField] private GameObject pfFireball; // 프리팹

    private Transform firePoint; // 투사체 발사 지점
    private Quaternion rotation; // 투사체 방향

    [SerializeField] private LayerMask levelCollisionLayer; // 콜리전 레이어
    [SerializeField] private LayerMask targetLayer; //타겟(적) 레이어



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        charStats = player.GetComponent<CharacterStatsHandler>();
        UpdateStats();
    }

    private void UpdateStats()
    {
        charStats.CurrentStates.atk = charStats.CurrentStates.atk * atk;
        charStats.CurrentStates.fireRate = charStats.CurrentStates.fireRate * fireRate;
    }
    public void PerformAttack(Vector2 attackDirection)
    {
        //아이템 공격 로직
        Debug.Log("Annnnnnnnnnnd Fireball!");
        rotation = PlayerAttackController.Instance.rotation;
        attackDirection = PlayerAttackController.Instance.attackDirection;
        LaunchProjectile(attackDirection, rotation);
    }

    // 투사체 발사
    void LaunchProjectile(Vector2 direction, Quaternion rotation)
    {
        // 투사체 포지션 찾기
        firePoint = player.transform;

        
        GameObject projectile = Instantiate(pfFireball, firePoint.position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile();
        }
        else if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            HealthSystem healthSystem = collision.GetComponentInParent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-atk);
            }
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
