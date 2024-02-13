using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, IAttack
{
    //private PlayerAttackController _playerAttackController;

    //[SerializeField] AttackSO attackStat;

    //[SerializeField] private float speed = 4f;

    //private Rigidbody2D _rigidbody2D;

    //private void Awake()
    //{
    //    _rigidbody2D = GetComponent<Rigidbody2D>();
    //}

    public void PerformAttack(Vector2 attackDirection)
    {
        //아이템 공격 로직
        //_playerAttackController.CreateProjectile(attackStat);
        Debug.Log("Annnnnnnnnnnd Fireball!");

        //float fireRate = attackStat.fireRate;
        //float damage = attackStat.damage;

        //_rigidbody2D.velocity = attackDirection.normalized * speed;

    }

}
