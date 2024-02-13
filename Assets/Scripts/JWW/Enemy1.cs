using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy1 : EnemyController
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movementDirection = Vector2.zero;
    private Collider2D _collider;
    public bool isContect = false;
    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    protected override void Start()
    {
        base.Start();
        OnMoveEvent += Move;
        OnAttackEvent += Attack;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector2 direction = Vector2.zero;
        if (!isContect)//접촉하지 않았을 때 공격중이 아닐 때
        {
            if(!isAttacking)
                direction = GetDirection();
        }
        ApplyMovement(_movementDirection);
        CallMoveEvent(direction);
        Rotate(direction);
    }
    public void Attack()
    {
    }
    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }
    public void ApplyMovement(Vector2 direction)
    {
        direction = direction * 2f;//몬스터의 스피드 만큼 속도 적용
        _rigidbody.velocity = direction;
    }   
    private void Rotate(Vector2 direction)
    {
        if (direction != Vector2.zero)//
        {
            int num = direction.x < 0 ? 1 : -1;
            transform.localScale = new Vector3(num, 1, 1);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
