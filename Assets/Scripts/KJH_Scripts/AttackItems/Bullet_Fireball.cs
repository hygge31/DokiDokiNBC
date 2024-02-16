using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Fireball : Bullet
{    
    private Rigidbody2D rb;

    private Animator animator; // 폭발 애니메이션을 위함
    private BoxCollider2D boxCollider2D;

    
    private float aliveTime = 0f;
    
    private Vector2 attackDirection;
    private int pCount;
    [SerializeField] private AudioClip fireballClip;
    [SerializeField] private AudioClip explodeClip;
    [SerializeField] private AudioClip pierceClip;

    private bool isHit = false;
    HealthSystem recentlyHitEnemey = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        aliveTime += Time.deltaTime;
        if (aliveTime >= Duration)
            Clear();
    }

    private void FixedUpdate()
    {
        OnFire(attackDirection);        
    }

    public override void Setup(Vector3 spawnPos, Vector2 dir, float atk, float travelSpeed, float duration, int pierceCount)
    {
        base.Setup(spawnPos, dir, atk, travelSpeed, duration, pierceCount);

        transform.position = spawnPos;
        transform.rotation = PlayerAttackController.Instance.rotation;
        attackDirection = dir;
        pCount = pierceCount + 1;
        SoundManager.Instance.PlayClip(fireballClip);
        recentlyHitEnemey = null;
    }

    private void OnFire(Vector2 dir)
    {
        rb.velocity = dir * TravelSpeed;
        if (isHit)
        {
            boxCollider2D.enabled = false;
            rb.velocity = Vector3.zero;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            isHit = true;
        }

        if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            //최근 맞춘 적 관통할 때 중복 충돌을 체크하기 위함
            //체력감소            
            HealthSystem healthSystem = collision.GetComponentInParent<HealthSystem>();
            if (healthSystem != null)
            {
                if (healthSystem != recentlyHitEnemey)
                {
                    healthSystem.ChangeHealth(-Atk);
                    pCount--;
                    Debug.Log("관통");
                    recentlyHitEnemey = healthSystem;
                    if(pCount > 0)
                    {
                        SoundManager.Instance.PlayClip(pierceClip);
                    }
                }

                if (pCount == 0)
                {
                    Debug.Log("관통 종료");
                    isHit = true;
                }
            }          
        }

        if (isHit)
        {
            SoundManager.Instance.PlayClip(explodeClip);
            animator.SetTrigger("Explode");
        }
    }

    public void Clear()
    {
        isHit = false;
        boxCollider2D.enabled = true;
        aliveTime = 0;
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(100, 0, 0);
        transform.rotation = Quaternion.identity;

        Managers.RM.Destroy(gameObject);
    }

}
