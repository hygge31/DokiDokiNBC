using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet_Fireball : Bullet
{
    [SerializeField] private LayerMask targetLayer;
    private Rigidbody2D rb;

    private float aliveTime = 0f;
    private float duration = 5f;
    private float travelSpeed = 6f;
    private float atk = 1f;
    private Vector2 attackDirection;

    //public Bullet_Fireball(float atk, float fireRate, float travelSpeed) : base(atk, fireRate, travelSpeed)
    //{
    //    atk = 1f;
    //    fireRate = 1f;
    //    travelSpeed = 6f;
    //}

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        OnFire(attackDirection);
        aliveTime += Time.deltaTime;
        if (aliveTime >= duration)
            Clear();
    }

    public override void Setup(Vector3 spawnPos, Vector2 dir, float atk, float travelSpeed, float duration)
    {
        base.Setup(spawnPos, dir, atk, travelSpeed, duration);

        transform.position = spawnPos;
        transform.rotation = PlayerAttackController.Instance.rotation;
        attackDirection = dir;
        this.atk = atk;
        this.travelSpeed = travelSpeed;
        this.duration = duration;
    }

    private void OnFire(Vector2 dir)
    {
        rb.velocity = dir * travelSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            //체력감소
            Debug.Log("맞음");
            HealthSystem healthSystem = collision.GetComponentInParent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-atk);
            }
            Clear();
        }
    }

    public void Clear()
    {
        Debug.Log("사라짐");
        aliveTime = 0;
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(100, 0, 0);
        transform.rotation = Quaternion.identity;

        Managers.RM.Destroy(gameObject);
    }

}
