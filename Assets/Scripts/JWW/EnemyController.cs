using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; //움직이는 이벤트
    public event Action OnAttackEvent; // 공격 이벤트

    protected Transform target; //타겟의 트렌스폼
    [SerializeField]
    public bool isAttacking = false; //공격 가능한지
    public float attackTime = 0;
    public float attackDelay = 1f;
    [SerializeField] public string targetTag = "Player"; //공격 대상의 태그
    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        target = GameObject.FindWithTag(targetTag).transform;//플레이어의 위치를 가져온다.
    }
    protected virtual void FixedUpdate()
    {
        if (isAttacking && attackTime >= attackDelay )//시간이 딜레이보다 많아졌다면
        {
            CallAttackEvent();
            attackTime = 0;
        }
        else
        {
            attackTime += Time.deltaTime;
            attackTime = Mathf.Clamp(attackTime,0,1);
        }
    }
    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }
    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
    protected Vector3 GetDirection() //플레이어를 향한 방향벡터 반환
    {
        return (target.position - transform.position).normalized;
    }
    protected float GetDistance() // 플레이어와의 거리를 반환
    { 
        return Vector3.Distance(target.position, transform.position);
    }
}
