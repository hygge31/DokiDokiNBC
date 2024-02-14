using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLaser : IWeapon
{
    public void Attack(Vector2 origin, Vector2 dir)
    {
        Debug.Log("레이저 공격");
    }
}
