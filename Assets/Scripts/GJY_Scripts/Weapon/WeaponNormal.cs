using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNormal : IWeapon
{
    public void Attack(Vector2 origin, Vector2 dir)
    {
        Debug.Log("일반 공격");
    }
}
