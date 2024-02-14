using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrenade : IWeapon
{
    public void Attack(Vector2 origin, Vector2 dir)
    {
        Debug.Log("수류탄 공격");
    }
}
