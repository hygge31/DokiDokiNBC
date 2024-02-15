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

    private bool isHit = false;


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
        if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            //체력감소            
            HealthSystem healthSystem = collision.GetComponentInParent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-Atk);
            }
            isHit = true;
            if (isHit)
            {
                animator.SetTrigger("Explode");
            }
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
