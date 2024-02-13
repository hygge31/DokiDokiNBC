using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : EnemyAnimation
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking"); //파라미터를 해시로 변환
    private static readonly int IsAttack = Animator.StringToHash("IsAttack"); //파라미터를 해시로 변환

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        enemyController.OnMoveEvent += Move; // 이동 구독
        enemyController.OnAttackEvent += Attack;// 공격 구독
    }

    private void Attack() //공격
    {
        animator.SetTrigger(IsAttack); // 공격 애니메이션 트리거 발동
    }

    private void Move(Vector2 direction) // 이동 
    {
        animator.SetBool(IsWalking, direction.magnitude > 0.5f);
    }
}
