using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJY_PlayerTemp : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public int extraHp;

    public float atk;
    public float fireRate;
    public float crit;    
    public float moveSpeed;

    private Rigidbody2D _rigid;    

    private bool isMovable = true;

    private Vector2 _fireDir;

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
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Managers.Attack.UseWeapon(transform.position, _fireDir);
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
