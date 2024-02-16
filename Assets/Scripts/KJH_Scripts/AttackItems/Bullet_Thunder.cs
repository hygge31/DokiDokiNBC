using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bullet_Thunder : Bullet
{
    private CircleCollider2D circleCollider;

    private float distance = 3f;

    private Vector2 attackDirection;
    private Vector3 newPosition;

    [SerializeField] private AudioClip fireClip;


    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
    }

    public override void Setup(Vector3 spawnPos, Vector2 dir, float atk, float travelSpeed, float duration, int pierceCount)
    {
        base.Setup(spawnPos, dir, atk, travelSpeed, duration, pierceCount);

        transform.position = spawnPos;
        attackDirection = dir;

        PositionAdjust(spawnPos);
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
        }
    }

    private void PositionAdjust(Vector3 spawnPos)
    {
        transform.position = spawnPos;
        newPosition = transform.position;

        if (attackDirection == Vector2.right)
        {
            newPosition.x += distance; // 오른쪽으로 이동
        }
        else if (attackDirection == Vector2.left)
        {
            newPosition.x -= distance; // 왼쪽으로 이동
        }
        else if (attackDirection == Vector2.up)
        {
            newPosition.y += distance; // 위쪽으로 이동
        }
        else if (attackDirection == Vector2.down)
        {
            newPosition.y -= distance; // 아래쪽으로 이동
        }

        transform.position = newPosition;

    }

    private void OnStrike()
    {
        SoundManager.Instance.PlayClip(fireClip);
        circleCollider.enabled = true;
    }

    private void OnDisappear()
    {
        circleCollider.enabled = false;
    }

    public void Clear()
    {
        transform.position = new Vector3(100, 0, 0);
        transform.rotation = Quaternion.identity;

        Managers.RM.Destroy(gameObject);
    }
}
