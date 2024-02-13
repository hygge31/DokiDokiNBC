using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJY_PlayerTemp : MonoBehaviour
{
    public float atk;
    public float fireRate;
    public float crit;    
    public float moveSpeed;

    private Rigidbody2D _rigid;

    private bool isMovable = true;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();

        Managers.Player.OnApplyPerkStat -= ApplyPerkStat;
        Managers.Player.OnApplyPerkStat += ApplyPerkStat;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isMovable = !isMovable;
            // Temp To Do - 상점 UI 출력
        }
    }

    private void Move()
    {
        if (!isMovable)
            return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movePos = new Vector2(moveX, moveY);
        Vector2 nextPos = (Vector2)transform.position + movePos.normalized * moveSpeed * Time.fixedDeltaTime;

        _rigid.MovePosition(nextPos);
    }

    private void ApplyPerkStat(Item_SO item)
    {
        atk += item.atk;
        fireRate += item.fireRate;
        crit += item.crit;
        moveSpeed += item.moveSpeed;
    }
}
